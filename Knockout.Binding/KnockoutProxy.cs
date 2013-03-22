using System;

namespace KnckoutBindingGenerater
{
    public class KnockoutProxy
    {
        private readonly string m_ViewModelName;
        private readonly string m_PrimitiveObservables;
        private readonly string m_MethodProxies;
        private readonly string m_CollectionObservables;

        const string c_ViewModelTemplate = @"
                var {0}ProxyObject = function() {{
                    {1}

                    {2}

                    {3}
                }}

                window.{4} = new {0}ProxyObject();
        
                ko.applyBindings({4});
            "; 

        public KnockoutProxy(string viewModelName, string primitiveObservables, string methodProxies, string collectionObservables)
        {
            m_ViewModelName = viewModelName;
            m_PrimitiveObservables = primitiveObservables;
            m_MethodProxies = methodProxies;
            m_CollectionObservables = collectionObservables;
        }

        public string KnockoutViewModel
        {
            get
            {
                return String.Format(c_ViewModelTemplate,
                                     m_ViewModelName,
                                     m_PrimitiveObservables,
                                     m_MethodProxies,
                                     m_CollectionObservables,
                                     ViewModelInstanceName);
            }
        }

        public string ViewModelInstanceName
        {
            get { return String.Format("{0}ProxyObjectInstance", m_ViewModelName); }
        }
    }
}