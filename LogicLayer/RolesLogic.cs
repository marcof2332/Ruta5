﻿using System;
using System.Collections.Generic;
using System.Linq;

using DataLayer;

namespace LogicLayer
{
    internal class RolesLogic : Interfaces.IRoleLogic
    {
        #region RolesLogicSingleton
        private static RolesLogic _instance = null;
        private RolesLogic() { }
        public static RolesLogic GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RolesLogic();
            }
            return _instance;
        }
        #endregion

        #region Methods
        public Roles RSearch(string code)
        {
            return (DbContextSingleton.TransporteContext.Roles.Where(r => r.Code == code).FirstOrDefault());

        }
        public void RAdd(Roles ro)
        {
            try
            {
                Validations.RoleValidation(ro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                DbContextSingleton.TransporteContext.Roles.Add(ro);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                DbContextSingleton.TransporteContext.Entry(ro).State = System.Data.Entity.EntityState.Detached;
                if (ex.InnerException != null)
                {
                    throw new Exception("Ocurrio un error en BD al intentar agregar el rol, por favor intente nuevamente.");
                }
                else
                    throw ex;
            }
        }
        public void RModify(Roles ro)
        {
            Roles modRo = null;
            try
            {
                modRo = DbContextSingleton.TransporteContext.Roles.Where(rol => rol.Code == ro.Code).FirstOrDefault();
                modRo.RolesDescription = ro.RolesDescription;
                Validations.RoleValidation(modRo);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RDelete(string ro)
        {
            try
            {
                Roles var = DbContextSingleton.TransporteContext.Roles.Where(r => r.Code == ro).FirstOrDefault();

                System.Data.SqlClient.SqlParameter _code = new System.Data.SqlClient.SqlParameter("@Code", ro);
                System.Data.SqlClient.SqlParameter _ret = new System.Data.SqlClient.SqlParameter("@ret", System.Data.SqlDbType.Int);
                _ret.Direction = System.Data.ParameterDirection.Output;
                DbContextSingleton.TransporteContext.Database.ExecuteSqlCommand("exec DeleteRole @Code, @ret output", _code, _ret);

                if ((int)_ret.Value == -1)
                    throw new Exception("No se encontro el rol, por favor intente nuevamente.");
                else if ((int)_ret.Value == -2)
                    throw new Exception("Un usuario tiene el rol asignado en el sistema, no se puede realizar la baja.");
                else if ((int)_ret.Value == -3)
                    throw new Exception("Ocurrio un error interno al realizar la baja, por favor intente nuevamente mas tarde.");
                else
                    DbContextSingleton.TransporteContext.Entry(var).State = System.Data.Entity.EntityState.Detached;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Roles> RList()
        {
            try
            {
                using (var dbContext = new TransportEntities())
                {
                    return (DbContextSingleton.TransporteContext.Roles.ToList());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
