using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TesteFluentNhibernate.DTO
{
    public class Usuario
    {
        #region Membros privados

        private int _id;
        private string _nome;
        private string _email;
        private Status _status;
        private string _statusAux;

        #endregion

        #region Construtor

        public Usuario()
        {
            _nome = String.Empty;
            _email = String.Empty;
            _status = Status.Ativo;
        }

        #endregion

        #region Propriedades públicas

        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public virtual string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public virtual Status Status
        {
            get { return (Status)Enum.Parse(typeof(Status), Enum.GetName(typeof(Status), Convert.ToChar(_statusAux))); }
            set { _status = value; }

            //set { _status = value; }
        }

        protected internal virtual string StatusAux
        {
            get
            {
                return _statusAux;
            }
            set
            {
                _statusAux = value;

                switch (_statusAux)
                {
                    case "A":
                        _status = DTO.Status.Ativo;
                        break;
                    case "I":
                        _status = DTO.Status.Inativo;
                        break;
                    case "B":
                        _status = DTO.Status.Bloqueado;
                        break;
                }
            }

            //get { return ((char)_status).ToString(); }           
        }

        #endregion
    }
}
