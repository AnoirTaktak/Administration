using Administration.Models;
using System.ComponentModel.DataAnnotations;

public class ClientDto
{

    [Required]
    [RegularExpression(@"^\d{13}$", ErrorMessage = "Le matricule fiscal doit comporter exactement 13 chiffres.")]
    public required string MF_Client { get; set; } // Matricule Fiscale


    [Required]
    [StringLength(100, ErrorMessage = "La raison sociale ne doit pas dépasser 100 caractères.")]
    public required string RS_Client { get; set; } // Raison Sociale

    [Required]
    [StringLength(200, ErrorMessage = "L'adresse ne doit pas dépasser 200 caractères.")]
    public required string Adresse_Client { get; set; }

    [Required]
    [Phone(ErrorMessage = "Le format du téléphone est invalide.")]
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Le numéro de téléphone doit comporter entre 10 et 15 chiffres.")]
    public required string Tel_Client { get; set; }

    [Required]
    [EnumDataType(typeof(TypeClient))]
    public required TypeClient Type_Client { get; set; } = TypeClient.PersonnePhysique; // Type de client PF ou PM
}
