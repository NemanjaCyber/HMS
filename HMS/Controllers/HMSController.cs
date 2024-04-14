using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HMSController:ControllerBase
    {
        public HMSContext Context { get; set; }

        public HMSController(HMSContext context)
        {
            Context = context;
        }

        [HttpPost("AddPatient")]
        public async Task<ActionResult> AddPatient([FromBody] Patient patient)
        {
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

            Context.Patients!.Add(p);
            await Context.SaveChangesAsync();
            return Ok(p);
        }

        [HttpGet("GetAllPatients")]
        public async Task<ActionResult> GetAllPatients()
        {
            var podaci = await Context.Patients!.Select(x => new
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

            var mh=await Context.MedicalHistories!.Where(x=>x.Patient_Id==p).FirstOrDefaultAsync();
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

        [HttpPost("AddMedicalHistory/{patientId}")]
        public async Task<ActionResult> AddMedicalHistory(int patientId, [FromQuery]string? alergies=null, [FromQuery]string? preConditions = null)
        {
            var mh = new MedicalHistory();
            mh.Patient_Id = await Context.Patients!.Where(x=>x.Patient_ID==patientId).FirstOrDefaultAsync();

            mh.Alergies = alergies ?? "Nema";
            mh.Pre_Conditions = preConditions ?? "Nema";

            Context.MedicalHistories!.Add(mh);
            await Context.SaveChangesAsync();
            return Ok("Dodate su moguce alergije i bolesti za pacijenta sa ID-jem: " + patientId);
        }
    }
}
