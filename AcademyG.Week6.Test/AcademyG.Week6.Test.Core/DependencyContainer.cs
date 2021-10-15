using Ninject;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyG.Week6.Test.Core
{
    public class DependencyContainer
    {
        //Inizializzazione statica
        private static readonly Lazy<IKernel> _Kernel = new Lazy<IKernel>(() => new StandardKernel());



        public static IKernel Kernel { get { return new StandardKernel(); } }



        /// <summary>
        /// Emette una istanza di un provider che implementa l'interfaccia richiesta
        /// </summary>
        /// <typeparam name="TInterface">Tipo di interfaccia del provider</typeparam>
        /// <returns>Ritorna una instanza (es. MongoDalProduct)</returns>
        public static TInterface Resolve<TInterface>()
        {
            //Ritorno la risoluzione
            return _Kernel.Value.Get<TInterface>();
        }



        /// <summary>
        /// Esegue la registrazione di un particolare interfaccia di
        /// reposotory associata alla sua implementazione
        /// </summary>
        /// <typeparam name="TRepositoryInterface">Tipo del'interfaccia</typeparam>
        /// <typeparam name="TRepositoryImplementation">Tipo dell'implementazione</typeparam>
        public static void Register<TRepositoryInterface, TRepositoryImplementation>()
        where TRepositoryImplementation : class, TRepositoryInterface, new()
        {
            //Espongo il metodo ed ottengo la sintassi per il bindind
            //di destinazione per l'interfaccia passata
            var bindingToSyntax = _Kernel.Value.Rebind<TRepositoryInterface>();



            //Eseguo il binding della sintassi al target
            var bindingOnSyntax = bindingToSyntax.To<TRepositoryImplementation>();



            //Applico la policy di singleton per la cache
            bindingOnSyntax.InTransientScope();
        }



        public static void Unregister<TInterface>()
        {
            _Kernel.Value.Unbind<TInterface>();
        }
    }
}
