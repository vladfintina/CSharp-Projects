using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VladFintina_FinalProject.Domain;

namespace VladFintina_FinalProject.Repository
{
    public interface Repository<E>
    {
        /*** 
         * @param element - the element which will be added
         *                  it must not be null
         * ***/
        public void addElement(E element);

        /*** 
         * @return all the elements
         * ***/
        public List<E> getElements();

        /***
         * updates the existing element with a new one
         * @param element - must not be null
         * @throws ExistanceException if the element does not exist in repository 
         * ***/
        public void modifyElement(E element);
        
    }
}
