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
    public class AuteursApiController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/AuteursApi
        public IQueryable<Auteur> GetAuteur()
        {
            return db.Auteur;
        }

        // GET: api/AuteursApi/5
        [ResponseType(typeof(Auteur))]
        public IHttpActionResult GetAuteur(int id)
        {
            Auteur auteur = db.Auteur.Find(id);
            if (auteur == null)
            {
                return NotFound();
            }

            return Ok(auteur);
        }

        // PUT: api/AuteursApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuteur(int id, Auteur auteur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != auteur.IdAuteur)
            {
                return BadRequest();
            }

            db.Entry(auteur).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuteurExists(id))
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

        // POST: api/AuteursApi
        [ResponseType(typeof(Auteur))]
        public IHttpActionResult PostAuteur(Auteur auteur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Auteur.Add(auteur);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = auteur.IdAuteur }, auteur);
        }

        // DELETE: api/AuteursApi/5
        [ResponseType(typeof(Auteur))]
        public IHttpActionResult DeleteAuteur(int id)
        {
            Auteur auteur = db.Auteur.Find(id);
            if (auteur == null)
            {
                return NotFound();
            }

            db.Auteur.Remove(auteur);
            db.SaveChanges();

            return Ok(auteur);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuteurExists(int id)
        {
            return db.Auteur.Count(e => e.IdAuteur == id) > 0;
        }
    }
}