using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ORM;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace FRONT.Controllers
{
    public class LivresController : Controller
    {
        private Model1 db = new Model1();

        private readonly string baseUrl = "https://localhost:44389";

        // GET: Livres
        public async Task<ActionResult> Index()
        {
            string url = baseUrl + "/api/LivresApi";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                //Test de succès
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Une erreur est apparue ma gueule");
                }

                var livre = await response.Content.ReadAsAsync<IEnumerable<Livre>>();

                return View(livre);
            }
        }

        // GET: Livres/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string url = baseUrl + "/api/LivresApi/" + id;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                //Test de succès
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Une erreur est apparue ma gueule");
                }

                var livre = await response.Content.ReadAsAsync<Livre>();

                if (livre == null)
                {
                    return HttpNotFound();
                }


                return View(livre);

            }
        }

        // GET: Livres/Create
        public ActionResult Create()
        {
            ViewBag.IdAuteur = new SelectList(db.Auteur, "IdAuteur", "Nom");
            return View();
        }

        // POST: Livres/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdLivre,Titre,Prix,IdAuteur,Genre")] Livre livre)
        {
            if (ModelState.IsValid)
            {

                //serialiser l'objet livre en json
                string json = JsonConvert.SerializeObject(livre);

                using (HttpClient client = new HttpClient())
                {
                    string url = baseUrl + "/api/LivresApi/" + livre.IdAuteur;

                    using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                    {
                        //Encodage de la requete
                        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                        request.Content = stringContent;

                        //envoyer la requete
                        var send = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                            .ConfigureAwait(false)
                        ;

                        if (!send.IsSuccessStatusCode)
                        {
                            throw new Exception("Une erreur est apparue ma gueule");
                        }

                        send.EnsureSuccessStatusCode();

                        return RedirectToAction("Index");
                    }
                }
            }

            return View(livre);
        }

        // GET: Livres/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string url = baseUrl + "/api/LivresApi/" + id;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                //Test de succès
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Une erreur est apparue ma gueule");
                }

                var livre = await response.Content.ReadAsAsync<Livre>();

                if (livre == null)
                {
                    return HttpNotFound();
                }

                ViewBag.IdAuteur = new SelectList(db.Auteur, "IdAuteur", "Nom", livre.IdAuteur);

                return View(livre);
            }
        }

        // POST: Livres/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdLivre,Titre,Prix,IdAuteur,Genre")] Livre livre)
        {
            if (ModelState.IsValid)
            {

                //serialiser l'objet livre en json
                string json = JsonConvert.SerializeObject(livre);

                using (HttpClient client = new HttpClient())
                {
                    string url = baseUrl + "/api/LivresApi/" + livre.IdAuteur;

                    using (var request = new HttpRequestMessage(HttpMethod.Put, url))
                    {
                        //Encodage de la requete
                        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                        request.Content = stringContent;

                        //envoyer la requete
                        var send = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                            .ConfigureAwait(false)
                        ;

                        if (!send.IsSuccessStatusCode)
                        {
                            throw new Exception("Une erreur est apparue ma gueule");
                        }

                        send.EnsureSuccessStatusCode();

                        return RedirectToAction("Index");
                    }
                }
            }
            ViewBag.IdAuteur = new SelectList(db.Auteur, "IdAuteur", "Nom", livre.IdAuteur);

            return View(livre);
        }

        // GET: Livres/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string url = baseUrl + "/api/LivresApi/" + id;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                //Test de succès
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Une erreur est apparue ma gueule");
                }

                var livre = await response.Content.ReadAsAsync<Livre>();

                if (livre == null)
                {
                    return HttpNotFound();
                }


                return View(livre);
            }
        }

        // POST: Livres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {

                //serialiser l'objet livre en json
                string json = JsonConvert.SerializeObject(id);

                using (HttpClient client = new HttpClient())
                {
                    string url = baseUrl + "/api/LivresApi/" + id;

                    using (var request = new HttpRequestMessage(HttpMethod.Delete, url))
                    {
                        //Encodage de la requete
                        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                        request.Content = stringContent;

                        //envoyer la requete
                        var send = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                            .ConfigureAwait(false)
                        ;

                        if (!send.IsSuccessStatusCode)
                        {
                            throw new Exception("Une erreur est apparue ma gueule");
                        }

                        send.EnsureSuccessStatusCode();


                        return RedirectToAction("Index");
                    }
                }
            }

            return View("index");
        }
    }
}
