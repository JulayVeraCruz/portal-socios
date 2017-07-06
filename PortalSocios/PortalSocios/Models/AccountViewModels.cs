using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalSocios.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Lembrar este browser?")]
        public bool RememberBrowser { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Palavra-passe")]
        public string Password { get; set; }

        [Display(Name = "Manter a sessão iniciada")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A palavra-chave deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Palavra-chave")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar palavra-chave")]
        [Compare("Password", ErrorMessage = "A palavra-chave e a confirmação da palavra-chave não correspondem.")]
        public string ConfirmPassword { get; set; }

        public Socios Socio { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Palavra-chave")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar palavra-chave")]
        [Compare("Password", ErrorMessage = "A palavra-chave e a confirmação da palavra-chave não correspondem.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}