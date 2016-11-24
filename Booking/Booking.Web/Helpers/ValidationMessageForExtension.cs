using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Booking.Web.Helpers
{
    public static class ValidationMessageForExtension
    {
        public static MvcHtmlString ExclamationValidationMessageFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            var standardMvcHtmlString = htmlHelper.ValidationMessageFor(expression);
            return GenerateTemplate(standardMvcHtmlString);
        }

        public static MvcHtmlString ExclamationValidationMessageFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string validationMessage)
        {
            var standardMvcHtmlString = htmlHelper.ValidationMessageFor(expression, validationMessage);
            return GenerateTemplate(standardMvcHtmlString);
        }

        public static MvcHtmlString ExclamationValidationMessageFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string validationMessage,
            object htmlAttributes)
        {
            var standardMvcHtmlString = htmlHelper.ValidationMessageFor(expression, validationMessage, htmlAttributes);
            return GenerateTemplate(standardMvcHtmlString);
        }

        private static MvcHtmlString GenerateTemplate(MvcHtmlString standardMvcHtmlString)
        {
            var messageHtml = standardMvcHtmlString.ToString();
            TagBuilder containerSpanBuilder = new TagBuilder(messageHtml);

            var text = containerSpanBuilder.InnerHtml;
            containerSpanBuilder.InnerHtml = "<i class=\"fa fa-exclamation-circle\" aria-hidden=\"true\"></i> " + text;

            return MvcHtmlString.Create(containerSpanBuilder.ToString(TagRenderMode.Normal));
        }
    }
}