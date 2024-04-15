using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HMSController : ControllerBase
    {
        public HMSContext Context { get; set; }

        public HMSController(HMSContext context)
        {
            Context = context;
        }

        [HttpPost("AddPatient/{roomID}")]
        public async Task<ActionResult> AddPatient([FromBody] Patient patient,int roomID)
        {
            var existingPatient = await Context.Patients!
                .FirstOrDefaultAsync(x => x.JMBG == patient.JMBG);
            if (existingPatient != null)
            {
                return BadRequest("Pacijent sa istim JMBG-om već postoji.");
            }

            var p = new Patient();
            p.Admision_Date = DateTime.Now;
            p.Patient_Fname=patient.Patient_Fname;
            p.Patient_Lname=patient.Patient_Lname;
            p.JMBG=patient.JMBG;
            p.Gender= patient.Gender;
            p.Blood_type= patient.Blood_type;
            p.Email=patient.Email;
            p.Phone=patient.Phone;
            p.Condition=patient.Condition;
            p.Discharge_Date=default(DateTime);
            var r= await Context.Rooms!
                .Where(x => x.Room_ID == roomID)
                .FirstOrDefaultAsync();
            if(r!.Room_Available_Beds>0)
            {
                p.Assigned_Room_Id = r;
                r.Room_Available_Beds--;
                Context.Rooms!.Update(r);
                await Context.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Zahtevana soba nema vise slobodnih kreveta.");
            }

            Context.Patients!.Add(p);
            await Context.SaveChangesAsync();
            return Ok(p);
        }

        [HttpGet("GetAllPatients")]
        public async Task<ActionResult> GetAllPatients()
        {
            var podaci = await Context.Patients!
                .Select(x => new
            {
                PatientFirstName = x.Patient_Fname,
                PatientLastName = x.Patient_Lname,
                PatientJMBG = x.JMBG,
                PatientGender=x.Gender,
                PatientBloodType = x.Blood_type,
                PatientEmail = x.Email,
                PatientPhone=x.Phone,
                PatientCondition=x.Condition,
                PatientAdmisionDate=x.Admision_Date
            }).ToListAsync();

            return Ok(podaci);
        }

        [HttpDelete("DeletePatient/{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {

            var p=await Context.Patients!.Where(x=>x.Patient_ID==id).FirstOrDefaultAsync();

            var mh=await Context.MedicalHistories!
                .Where(x=>x.Patient_Id==p)
                .FirstOrDefaultAsync();
            Context.MedicalHistories!.Remove(mh);
            await Context.SaveChangesAsync();

            if(p==null)
            {
                return BadRequest("Pacijent sa unesenim ID-jem ne postoji");
            }    
            else
            {
                Context.Patients!.Remove(p);
                await Context.SaveChangesAsync();
                return Ok("Uklonjen pacijent sa Id-jem " +p.Patient_ID);
            }
        }

        [HttpPost("AddMedicalHistory/{patientJMBG}")]
        public async Task<ActionResult> AddMedicalHistory(string patientJMBG, [FromQuery]string? alergies=null, [FromQuery]string? preConditions = null)
        {
            var mh = new MedicalHistory();
            mh.Patient_Id = await Context.Patients!
                .Where(x=>x.JMBG==patientJMBG)
                .FirstOrDefaultAsync();

            mh.Alergies = alergies ?? "Nema";
            mh.Pre_Conditions = preConditions ?? "Nema";

            Context.MedicalHistories!.Add(mh);
            await Context.SaveChangesAsync();
            return Ok("Dodate su moguce alergije i bolesti za pacijenta sa JMBG-om: " + patientJMBG);
        }

        [HttpDelete("DischargePatient/{patientJMBG}")]
        public async Task<ActionResult> DischargePatient(string patientJMBG)
        {
            var p = await Context.Patients!
                .Where(x => x.JMBG == patientJMBG)
                .FirstOrDefaultAsync();
            if (p == null)
            {
                return BadRequest("Nepostojeci JMBG");
            }

            var mh = await Context.MedicalHistories!
                .Where(x => x.Patient_Id == p)
                .FirstOrDefaultAsync();
            if (mh != null)
            {
                Context.MedicalHistories!.Remove(mh!);
            }

            var r = await Context.Rooms!
                .Where(x => x.Room_Patients!.Contains(p))
                .FirstOrDefaultAsync();
            r!.Room_Available_Beds++;

            Context.Patients!.Remove(p);

            await Context.SaveChangesAsync();

            return Ok("Otpusten je pacijent sa JMBG-om: " + patientJMBG);
        }

        [HttpGet("ReturnAllRoomsWithAllPatients")]
        public async Task<ActionResult> ReturnAllRoomsWithAllPatients()
        {
            var podaci = await Context.Rooms!
                .Include(x => x.Room_Patients)
                .Select(x => new
                {
                    RoomType = x.Room_Type,
                    RoomCost = x.Room_Cost,
                    RoomAvailableBeds = x.Room_Available_Beds,
                    pacijenti = x.Room_Patients!.Select(x => new
                    {
                        PatientFirstName = x.Patient_Fname,
                        PatientLastName = x.Patient_Lname,
                        PatientJMBG = x.JMBG,
                        PatientGender = x.Gender,
                        PatientBloodType = x.Blood_type,
                        PatientEmail = x.Email,
                        PatientPhone = x.Phone,
                        PatientCondition = x.Condition,
                        PatientAdmisionDate = x.Admision_Date
                    })
                }).ToListAsync();

            return Ok(podaci);
        }

        [HttpGet("ReturnPatientsInRoom/{roomId}")]
        public async Task<ActionResult> ReturnPatientsInRoom(int roomId)
        {
            var podaci = await Context.Rooms!
                .Include(x => x.Room_Patients)
                .Where(x => x.Room_ID == roomId)
                .Select(x => new
                {
                    pacijenti = x.Room_Patients!.Select(x => new
                    {
                        PatientFirstName = x.Patient_Fname,
                        PatientLastName = x.Patient_Lname,
                        PatientJMBG = x.JMBG,
                        PatientGender = x.Gender,
                        PatientBloodType = x.Blood_type,
                        PatientEmail = x.Email,
                        PatientPhone = x.Phone,
                        PatientCondition = x.Condition,
                        PatientAdmisionDate = x.Admision_Date
                    })
                }).ToListAsync();

            return Ok(podaci);
        }

        //ne radi dobro
        //[HttpPut("UpdateMedicalHistory/{patientJMBG}")]
        //public async Task<ActionResult> UpdateMedicalHistory(string patientJMBG, [FromQuery] string? alergies = null, [FromQuery] string? preConditions = null)
        //{
        //    var p = Context.Patients!.Where(x => x.JMBG == patientJMBG).FirstOrDefault();
        //    var mh = await Context.MedicalHistories!.Where(x => x.Patient_Id == p).FirstOrDefaultAsync();

        //    if(mh!.Alergies==null)
        //    {
        //        mh.Alergies = alergies ?? "Nema";
        //    }
        //    if (mh!.Pre_Conditions == null)
        //    {
        //        mh.Pre_Conditions = preConditions ?? "Nema";
        //    }

        //    Context.MedicalHistories!.Update(mh);
        //    await Context.SaveChangesAsync();
        //    return Ok("Azuriran je pacijent sa JMBG-om " + patientJMBG);
        //}


    }
}
