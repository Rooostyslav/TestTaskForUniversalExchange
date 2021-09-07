using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskForUniversalExchange.Models
{
	[Table("Applications")]
	public class Application
	{
		[Key, Column(Order = 1)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required(ErrorMessage = "Поле обязательное для заполнения.")]
		[StringLength(40, ErrorMessage = "Максимальная длина Фамилии и Имени 40 символов.")]
		[RegularExpression(@"^[А-Я][а-я]+\s[А-Я][а-я]+$",
			ErrorMessage = "Поле не соответствует формату \"Фамилия Имя\". Только кирилица.")]
		[Column(Order = 2)]
		public string FullName { get; set; }

		[Required(ErrorMessage = "Поле обязательное для заполнения.")]
		[Range(0, 99, ErrorMessage = "Возраст должен быть от {1} до {2}.")]
		[Column(Order = 3)]
		public int Age { get; set; }

		[Required(ErrorMessage = "Поле обязательное для заполнения.")]
		[Range(0, 99, ErrorMessage = "Стаж должен быть от {1} до {2}.")]
		[Column(Order = 4)]
		public int Experience { get; set; }

		[Required(ErrorMessage = "Поле обязательное для заполнения.")]
		[DataType(DataType.EmailAddress, ErrorMessage = "Електронный адрес должен соответствовать шаблону.")]
		[Column(Order = 5)]
		public string Email { get; set; }

		[Required(ErrorMessage = "Поле обязательное для заполнения.")]
		[StringLength(13, ErrorMessage = "Максимальная длина {1} символов.")]
		[RegularExpression(@"^\+380\d{9}$", 
			ErrorMessage = "Мобильный телефон должен соответствовать шаблону +380XXXXXXXXX.")]
		[Column(Order = 6)]
		public string MobilePhone { get; set; }

		[StringLength(1000, ErrorMessage = "Максимальное количество {1} символов.")]
		[Column(Order = 7)]
		public string Notes { get; set; }
	}
}
