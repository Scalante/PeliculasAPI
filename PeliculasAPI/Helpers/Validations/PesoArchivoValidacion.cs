using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Helpers.Validations
{
    public class PesoArchivoValidacion: ValidationAttribute
    {
        private readonly int _pesoMaximoMegaBytes;

        public PesoArchivoValidacion(int pesoMaximoMegaBytes)
        {
            this._pesoMaximoMegaBytes = pesoMaximoMegaBytes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            if (formFile.Length > _pesoMaximoMegaBytes * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {_pesoMaximoMegaBytes}mb");
            }

            return ValidationResult.Success;
           
        }
    }
}
