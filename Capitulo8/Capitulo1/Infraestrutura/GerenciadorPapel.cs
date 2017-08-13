using Capitulo1.Areas.Seguranca.Models;
using Capitulo1.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capitulo1.Infraestrutura
{
    public class GerenciadorPapel : RoleManager<Papel>, IDisposable
    {
        //São definidos dois métodos

        //um construtor que invoca o construtor da classe base
        public GerenciadorPapel(RoleStore<Papel> store) : base(store)
        {
        }

        //método estático que cria uma instância de AppRoleManager
        public static GerenciadorPapel Create(IdentityFactoryOptions<GerenciadorPapel> options, IOwinContext context)
        {
            return new GerenciadorPapel(new RoleStore<Papel>(context.Get<IdentityDbContextAplicacao>()));
        }
    }
}