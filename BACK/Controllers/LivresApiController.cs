using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ORM;

namespace BACK.Controllers
{
    public class LivresApiController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/LivresApi
        public IQueryable<Livre> GetLivre()
        {
            return db.Livre;
        }

        // GET: api/LivresApi/5
        [ResponseType(typeof(Livre))]
        public IHttpActionResult GetLivre(int id)
        {
            Livre livre = db.Livre.Find(id);
            if (livre == null)
            {
                return NotFound();
            }

            return Ok(livre);
        }

        // PUT: api/LivresApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLivre(int id, Livre livre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != livre.IdLivre)
            {
                return BadRequest();
            }

            db.Entry(livre).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/LivresApi
        [ResponseType(typeof(Livre))]
        public IHttpActionResult PostLivre(Livre livre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Livre.Add(livre);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = livre.IdLivre }, livre);
        }

        // DELETE: api/LivresApi/5
        [ResponseType(typeof(Livre))]
        public IHttpActionResult DeleteLivre(int id)
        {
            Livre livre = db.Livre.Find(id);
            if (livre == null)
            {
                return NotFound();
            }

            db.Livre.Remove(livre);
            db.SaveChanges();

            return Ok(livre);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LivreExists(int id)
        {
            return db.Livre.Count(e => e.IdLivre == id) > 0;
        }
    }
}