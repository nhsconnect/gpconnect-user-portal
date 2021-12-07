using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace gpconnect_user_portal.Helpers.Validators
{
    public class AtLeastOnePropertyAttribute: ValidationAttribute
    {
        private string[] PropertyList { get; set; }

        public AtLeastOnePropertyAttribute(params string[] propertyList)
        {
            this.PropertyList = propertyList;
        }

        public override object TypeId
        {
            get
            {
                return this;
            }
        }

        public override bool IsValid(object value)
        {
            PropertyInfo propertyInfo;
            foreach (string propertyName in PropertyList)
            {
                propertyInfo = value.GetType().GetProperty(propertyName);

                if (propertyInfo != null && propertyInfo.GetValue(value, null) != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
