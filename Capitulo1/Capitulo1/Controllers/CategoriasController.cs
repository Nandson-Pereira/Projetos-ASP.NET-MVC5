﻿using Capitulo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capitulo1.Controllers
{
    public class CategoriasController : Controller
    {
        private static IList<Categoria> categorias = new List<Categoria>()
        {
            new Categoria() {
                CategoriaId = 1,
                Nome = "Notebooks"
                },

            new Categoria() {
                CategoriaId = 2,
                Nome = "Monitores"
                },

            new Categoria() {
                CategoriaId = 3,
                Nome = "Impressoras"
                },

            new Categoria() {
                CategoriaId = 4,
                Nome = "Mouses"
                },

            new Categoria() {
                CategoriaId = 5,
                Nome = "Desktops"
                }
        };


        //GET: Detalhhes da Categorias
        //Comon ão haverá interação com o usuário na visão a ser gerada, implementaremos apenas a action HTTP GET
        public ActionResult Details(int id)
        {
            return View(categorias.Where(m => m.CategoriaId == id).First());
        }


        //GET: Delete Categorias
        public ActionResult Delete(int id)
        {
            return View(categorias.Where(m => m.CategoriaId == id).First());
        }



        //ACTION POST que recebe o modelo para deletar a categoria de fato.
        //Esta será responsável por deletar os dados informados na visão de deletar.
        //ACTION POST que  recebe os dados escolhidos pelo usuário para serem deletados.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Categoria categoria)
        {
            categorias.Remove(categorias.Where(m => m.CategoriaId == categoria.CategoriaId).First());

            return RedirectToAction("Index");
        }


        //GET: Edição da Categorias
        //ACTION GET de edição de dados no controlador. informar os dados a serem editados pelo id na visão.
        //action GET para ser gerada a visão de interação com o usuário.
        public ActionResult Edit(int id)
        {
            return View(categorias.Where(m => m.CategoriaId == id).First());
        }


        //ACTION POST que recebe o modelo para edição e salvar a alteração..
        //Esta será responsável por receber os dados informados na visão no editar escolhido.
        //ACTION POST que  recebe os dados escolhidos pelo usuário para serem alterados.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            //Forma 1 de realizar a operação de edição da categoria
            categorias.Remove(categorias.Where(c => c.CategoriaId == categoria.CategoriaId).First());
            categorias.Add(categoria);

            //Forma 2 de realizar a operação de edição da categoria
            //categorias[categorias.IndexOf(categorias.Where(c => c.CategoriaId == categoria.CategoriaId).First())] = categoria;

            return RedirectToAction("Index");
        }



        //GET: Criação da Categorias
        //ACTION GET de inserção de dados no controlador. informar os dados na visão.
        //action GET para ser gerada a visão de interação com o usuário.
        public ActionResult Create()
        {
            return View();
        }


        //ACTION POST que recebe o modelo para inserção.
        //Esta será responsável por receber os dados informados na visão.
        //ACTION POST que  recebe os dados inseridos pelo usuário.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            categorias.Add(categoria);
            categoria.CategoriaId = categorias.Select(m => m.CategoriaId).Max() + 1;

            return RedirectToAction("Index");
        }



        // GET: Categorias
        //ACTION principal onde é listada as categorias.
        public ActionResult Index()
        {
            return View(categorias.OrderBy(c => c.Nome));
        }
    }
}