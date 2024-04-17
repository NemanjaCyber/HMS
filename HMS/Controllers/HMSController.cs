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

            // Pozivanje druge za kreiranje kartona pri registraciji pacijenta u sistem
            var prescriptionDate = DateTime.Now;
            await CreatePrescriptionForPatient(p.Patient_ID, prescriptionDate);

            return Ok(p);
        }

        [HttpGet("GetAllPatientsWithAllData")]
        public async Task<ActionResult> GetAllPatients()
        {
            var podaci = await Context.Patients!
                .Select(x => new
                {
                    PatientFirstName = x.Patient_Fname,
                    PatientLastName = x.Patient_Lname,
                    PatientJMBG = x.JMBG,
                    PatientGender = x.Gender,
                    PatientBloodType = x.Blood_type,
                    PatientEmail = x.Email,
                    PatientPhone = x.Phone,
                    PatientCondition = x.Condition,
                    PatientAdmisionDate = x.Admision_Date,
                    PatientEmergencyContaxt = x.PatientsEmergencyContacts!.Select(x => new
                    {
                        EmergencyContaxtName = x.EmergencyContact_Name,
                        EmergencyContactPhone = x.EmergencyContact_Phone,
                        EmergencyContactRelation = x.EmergencyContact_Realtion
                    }),
                    PatientsPrescription = Context.Prescriptions!
                .Where(t => t.Assigned_Patient == x)
                .Select(y => new
                {
                    PrescriptionCreated = y.Date,
                    PrescriptedMedicines = y.Assigned_Medicines!
                    .Select(u => new
                    {
                        MedicineName = u.M_Name
                    })
                }).ToList()
                }).ToListAsync();

            return Ok(podaci);
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

        [HttpGet("ReturnMedicalHistoryForPatient/{patientId}")]
        public async Task<ActionResult> ReturnMedicalHistoryForPatient(int patientId)
        {
            var p=await Context.Patients!.Where(x=>x.Patient_ID==patientId).FirstOrDefaultAsync();
            var podaci = await Context.MedicalHistories!
                .Where(x => x.Patient_Id == p)
                .Select(x => new
                {
                    PatientAlergies = x.Alergies,
                    PatientPreConditions = x.Pre_Conditions
                }).ToListAsync();

            return Ok(podaci);
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

            var pres = await Context.Prescriptions!
                .Where(x => x.Assigned_Patient == p)
                .FirstOrDefaultAsync();
            if (pres != null)
            {
                Context.Prescriptions!.Remove(pres!);
            }

            var ec = await Context.EmergencyContacts!
                .Where(x => x.RelatedPatient_Id == p)
                .FirstOrDefaultAsync();
            if (ec != null)
            {
                Context.EmergencyContacts!.Remove(ec!);
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

        [HttpGet("ReturnAllMedicines")]
        public async Task<ActionResult> ReturnAllMedicines()
        {
            var podaci = await Context.Medicines!
                .Select(x => new
                {
                    MedicineName = x.M_Name,
                    MedicineQuantity = x.M_Quantity,
                    MedicineCost = x.M_Cost
                }).ToListAsync();

            return Ok(podaci);
        }

        [HttpPost("CreatePrescriptionForPatient/{patientId}/{date}")]
        public async Task<ActionResult> CreatePrescriptionForPatient(int patientId, DateTime date)
        {
            var p = await Context.Patients!
                .Where(x => x.Patient_ID == patientId)
                .FirstOrDefaultAsync();

            var pres = new Prescription();
            pres.Date= date;
            pres.Assigned_Patient = p;
            Context.Prescriptions!.Add(pres);
            await Context.SaveChangesAsync();
            return Ok("Dodat je recept");
        }

        [HttpPut("AddMedicineToPrescription/{prescriptionId}/{medicineId}")]
        public async Task<ActionResult> AddMedicineToPrescription(int prescriptionId, int medicineId)
        {
            var p = await Context.Prescriptions!
                .Include(x => x.Assigned_Medicines)
                .Where(x => x.Prescription_ID == prescriptionId)
                .FirstOrDefaultAsync();
            var m = await Context.Medicines!.Where(x => x.Medicine_ID == medicineId).FirstOrDefaultAsync();

            if(m.M_Quantity<=0)
            {
                return BadRequest("Trazeni lek nije vise na stanju");
            }
            m.M_Quantity--;
            Context.Medicines!.Update(m);

            if(p.Assigned_Medicines!.Contains(m))
            {
                return BadRequest("Receptu je vec dodeljen odabrani lek.");
            }

            p.Assigned_Medicines.Add(m);
            await Context.SaveChangesAsync();
            return Ok("Uspesno je dodat lek na recept" + p);
        }

        [HttpPost("AddEmergencyContactForPatient/{patientId}")]
        public async Task<ActionResult> AddEmergencyContactForPatient(int patientId, [FromBody] EmergencyContact ec)
        {
            var ecNew = new EmergencyContact();
            ecNew.EmergencyContact_Name = ec.EmergencyContact_Name;
            ecNew.EmergencyContact_Realtion = ec.EmergencyContact_Realtion;
            ecNew.EmergencyContact_Phone = ec.EmergencyContact_Phone;
            ecNew.RelatedPatient_Id = await Context.Patients!.Where(x => x.Patient_ID == patientId).FirstOrDefaultAsync();
            Context.EmergencyContacts!.Add(ecNew);
            await Context.SaveChangesAsync();
            return Ok(ecNew);
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
