using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using ORM;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace FRONT.Controllers
{
    public class AuteursController : Controller
    {
        private string baseUrl = "https://localhost:44389";

        // GET: Auteurs
        public async Task<ActionResult> Index()
        {
            string url = baseUrl + "/api/AuteursApi";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                //Test de succès
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Une erreur est apparue ma gueule");
                }

                var auteur = await response.Content.ReadAsAsync<IEnumerable<Auteur>>();

                return View(auteur);
            }
        }

        // GET: Auteurs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string url = baseUrl + "/api/AuteursApi/" + id;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                //Test de succès
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Une erreur est apparue ma gueule");
                }

                var auteur = await response.Content.ReadAsAsync<Auteur>();

                if (auteur == null)
                {
                    return HttpNotFound();
                }


                return View(auteur);
            }
        }

        // GET: Auteurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auteurs/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdAuteur,Nom,Prenom,DateNaissance")] Auteur auteur)
        {
            if (ModelState.IsValid)
            {

                //serialiser l'objet autheur en json
                string json = JsonConvert.SerializeObject(auteur);

                using (HttpClient client = new HttpClient())
                {
                    string url = baseUrl + "/api/AuteursApi/" + auteur.IdAuteur;

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

            return View(auteur);
        }

        // GET: Auteurs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string url = baseUrl + "/api/AuteursApi/" + id;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                //Test de succès
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Une erreur est apparue ma gueule");
                }

                var auteur = await response.Content.ReadAsAsync<Auteur>();

                if (auteur == null)
                {
                    return HttpNotFound();
                }


                return View(auteur);
            }
        }

        // POST: Auteurs/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdAuteur,Nom,Prenom,DateNaissance")] Auteur auteur)
        {
            if (ModelState.IsValid)
            {

                //serialiser l'objet autheur en json
                string json = JsonConvert.SerializeObject(auteur);

                using (HttpClient client = new HttpClient())
                {
                    string url = baseUrl + "/api/AuteursApi/" + auteur.IdAuteur;

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

            return View(auteur);
        }

        // GET: Auteurs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string url = baseUrl + "/api/AuteursApi/" + id;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                //Test de succès
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Une erreur est apparue ma gueule");
                }

                var auteur = await response.Content.ReadAsAsync<Auteur>();

                if (auteur == null)
                {
                    return HttpNotFound();
                }


                return View(auteur);
            }
        }

        // POST: Auteurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {

                //serialiser l'objet autheur en json
                string json = JsonConvert.SerializeObject(id);

                using (HttpClient client = new HttpClient())
                {
                    string url = baseUrl + "/api/AuteursApi/" + id;

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
