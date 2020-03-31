using System;
using System.Collections.Generic;

namespace DemoServer.Core
{
    public class enFactory<AbstractProduct, IdentifierType> where AbstractProduct : class
    {
        public AbstractProduct CreateObject(IdentifierType tId)
        {
            Type tProductType = null;
            if (m_aAssociations.TryGetValue(tId, out tProductType))
            {
                return Activator.CreateInstance(tProductType) as AbstractProduct;
            }
            return null;
        }

        public bool IsRegistered(IdentifierType tId)
        {
            return m_aAssociations.ContainsKey(tId);
        }

        protected bool Register(IdentifierType tId, Type tType)
        {
            if (IsRegistered(tId))
                return false;

            m_aAssociations.Add(tId, tType);
            return true;
        }

        protected bool Unregister(IdentifierType tId)
        {
            return m_aAssociations.Remove(tId);
        }

        public Dictionary<IdentifierType, Type>.KeyCollection Keys()
        {
            return m_aAssociations.Keys;
        }

        public IdentifierType AssociateKey(Type product)
        {
            foreach (var p in m_aAssociations) if (p.Value == product) return p.Key;

            return new KeyValuePair<IdentifierType, Type>().Key;
        }

        private Dictionary<IdentifierType, Type> m_aAssociations = new Dictionary<IdentifierType, Type>();

    };

}
