﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PassionProject_N01457602.Models;
using PassionProject_N01457602.Models.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Web.Script.Serialization;


namespace PassionProject_N01457602.Controllers
{
    public class PetController : Controller
    {
        //Http Client is the proper way to connect to a webapi
        //https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;


        static PetController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            client = new HttpClient(handler);
            //change this to match your own local port number
            client.BaseAddress = new Uri("https://localhost:44339/api/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);
        }



        // GET: Pet/List
        public ActionResult List()
        {
            string url = "petdata/getpets";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<PetDto> SelectedPets = response.Content.ReadAsAsync<IEnumerable<PetDto>>().Result;
                return View(SelectedPets);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Pet/Details/5
        public ActionResult Details(int id)
        {
            ShowPet ViewModel = new ShowPet();
            string url = "petdata/findpet/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into pet data transfer object
                PetDto SelectedPet = response.Content.ReadAsAsync<PetDto>().Result;
                ViewModel.pet = SelectedPet;


                url = "petdata/findcustomerforpet/" + id;
                response = client.GetAsync(url).Result;
                CustomerDto SelectedCustomer = response.Content.ReadAsAsync<CustomerDto>().Result;
                ViewModel.customer = SelectedCustomer;

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        // GET: Pet/Create
        public ActionResult Create()
        {

            return View();
        }



        // POST: Pet/Create
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(Pet PetInfo)
        {
            Debug.WriteLine(PetInfo.PetName);
            string url = "petdata/addpet";
            Debug.WriteLine(jss.Serialize(PetInfo));
            HttpContent content = new StringContent(jss.Serialize(PetInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {

                int petid = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = petid });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


         // GET: Pet/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "petdata/findpet/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into Customer data transfer object
                PetDto SelectedPet = response.Content.ReadAsAsync<PetDto>().Result;
                return View(SelectedPet);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        // POST: Pet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(int id, Pet PetInfo)
        {
            Debug.WriteLine(PetInfo.PetName);
            string url = "petdata/updatepet/" + id;
            Debug.WriteLine(jss.Serialize(PetInfo));
            HttpContent content = new StringContent(jss.Serialize(PetInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            { 

                response = client.PostAsync(url, content).Result;

                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Pet/Delete/5
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "petdata/findpet/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into pet data transfer object
                PetDto SelectedPet = response.Content.ReadAsAsync<PetDto>().Result;
                return View(SelectedPet);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Pet/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            string url = "petdata/deletepet/" + id;
            //post body is empty
            HttpContent content = new StringContent("");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Error()
        {
            return View();
        }
    }

}