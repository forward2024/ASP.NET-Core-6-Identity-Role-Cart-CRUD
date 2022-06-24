﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Forward.Models.model
{
    public class OrderFinish
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string AppUserId { get; set; }
        [ValidateNever]
        public AppUser AppUser { get; set; }
        public int ProductId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }
        public DateTime DateTimeOfOrder { get; set; }
        public string? OrderStatus { get; set; } = Forward.Models.OrderStatus.Status_0;
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public double CountProduct { get; set; }
        public double TotalPrice { get; set; }
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
