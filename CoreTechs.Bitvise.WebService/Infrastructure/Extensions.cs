using System;
using System.Reflection;
using System.ServiceProcess;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using FluentValidation;
using FluentValidation.Results;

namespace CoreTechs.Bitvise.WebService.Infrastructure
{
    static class Extensions
    {
        public static void Start(this ServiceBase service, params string[] args)
        {
            var onStart = typeof(ServiceBase).GetMethod("OnStart", BindingFlags.Instance | BindingFlags.NonPublic);
            onStart.Invoke(service, BindingFlags.Default, null, new object[] { args }, null);
        }

        public static void FillModelState(this ValidationResult val, ModelStateDictionary modelState)
        {
            modelState.Merge(val.ToModelStateDictionary());
        }

        public static void FillModelState(this ValidationResult val, ApiController controller)
        {
            val.FillModelState(controller.ModelState);
        }

        public static ModelStateDictionary ToModelStateDictionary(this ValidationResult val)
        {
            var dict = new ModelStateDictionary();

            foreach (var err in val.Errors)
                dict.AddModelError(err.PropertyName, err.ErrorMessage);

            return dict;
        }

        /// <summary>
        /// Validates the model, and fills modelstate if invalid.
        /// </summary>
        public static ValidationResult Validate(this ApiController controller, object model, IValidator validator)
        {
            var result = validator.Validate(model);

            if (!result.IsValid)
                result.FillModelState(controller);

            return result;
        }

        /// <summary>
        /// Constructs validator with default ctor, validates the model, and fills modelstate if invalid.
        /// </summary>
        public static ValidationResult Validate<TValidator>(this ApiController controller, object model) where TValidator : IValidator
        {
            var validator = Activator.CreateInstance<TValidator>();
            return controller.Validate(model, validator);
        }
    }
}
