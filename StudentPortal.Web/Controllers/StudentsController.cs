using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;
using System;
using System.Net.NetworkInformation;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Buffers.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace StudentPortal.Web.Controllers
{
	public class StudentsController : Controller
	{
		private readonly ApplicationDbContext dbContext;
		private readonly ILogger<StudentsController> logger;

		// Modify the constructor to accept the logger
		public StudentsController(ApplicationDbContext dbContext, ILogger<StudentsController> logger)
		{
			this.dbContext = dbContext;
			this.logger = logger;
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}
		// The issue you're facing could be related to a couple of things. From what you've shared, it seems that the custom validation you're adding (ModelState.AddModelError("CustomError", "Error as Name and Phone value is equal")) isn't being displayed as expected, even when the Name and Phone values are identical (e.g., 77776).
		// 
		// Potential Issue :
		//The problem could be related to the value in viewModel.Phone.If the Phone is an integer(e.g., int), it will be correctly converted to a string when using .ToString(). However, if Phone is a string and Name contains leading or trailing spaces, the comparison might fail because of those spaces.
		// 
		// ModelState.AddModelError("Key", "Content");
		//
		//  ModelState and Validation Summary : 
		// Ensure the ModelState error is being correctly added. You can debug this by checking the ModelState after the comparison to see if the error is indeed added.
		//Instead of using the "CustomError" key, try using an empty string (string.Empty) or a model property-based key, which might work better in some scenarios.
		//
		// Using string.Empty will ensure that the error is added to the general validation errors, rather than to a specific property, which might be why it isn't appearing in the validation summary.
		////
		//Summary Checklist:
		// // Fix ModelState Error Key: Use string.Empty instead of "CustomError" for general errors.
		// // Log ModelState: Add logging to see if the error is being added correctly.
		// // Ensure Client-Side Validation Scripts: Ensure your page is rendering client-side validation scripts with @Html.RenderPartialAsync("_ValidationScriptsPartial").
		// // Check Model Binding and Values: Make sure the Phone and Name values are bound correctly, with no unexpected spaces.
		// // Validation Summary and Field Validation: Ensure both validation summary and field-specific validation are correctly rendered in the view.

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Add(AddStudentViewModel viewModel)
		{
			if (viewModel.Name.Trim() == viewModel.Phone.Trim())
			{
				ModelState.AddModelError(string.Empty, "Name and Phone cannot be the same.");
			}
			if (ModelState.IsValid)
			{
				//try
				//{
					// Create a new student from the view model
					var student = new Student
					{
						Name = viewModel.Name,
						Email = viewModel.Email,
						Phone = viewModel.Phone,
						Subscribed = viewModel.Subscribed,
					};
					await dbContext.Students.AddAsync(student);
					await dbContext.SaveChangesAsync();

					logger.LogInformation("New Information: {StudentName} ({StudentEmail})", student.Name, student.Email);

					TempData["insert_message"] = $"New Student -{student.Name} Added..";
					return RedirectToAction("List");
				//}
				//catch (Exception ex)
				//{
				//	logger.LogError(ex, ex.Message);

				//	// Return the same view with the model and display error
				//	ModelState.AddModelError(string.Empty, "An error occurred while saving the student.");
				//	return View(viewModel); // // Optionally, show the same view in case of error
				//}
			}
			ModelState.AddModelError(string.Empty, "An error occurred while saving the student.");
			// If ModelState is invalid, return the view with the model to show validation errors
			// @Html.ValidationSummary(true)
			//  
			// A list of model-level errors (from @Html.ValidationSummary(true))
			// Specific field errors(from @Html.ValidationMessageFor()) next to the form fields.
			return View(viewModel);
		}

		[HttpGet]
		public async Task<IActionResult> List()
		{
			var students = await dbContext.Students.ToListAsync();
			return View(students);
		}
		[HttpGet]
		public async Task<IActionResult> GetMoreDetails(Guid id)
		{
			//var student = await dbContext.Students.FindAsync(id);
			//var student = await dbContext.Students.FirstOrDefaultAsync(id);
			var student = await dbContext.Students.FirstOrDefaultAsync(s => s.Id == id);  // Find the student by ID
			if (student == null)
			{
				return NotFound();  // Return 404 if no student is found
			}
			return View(student);  // Pass the student to the view
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var student = await dbContext.Students.FindAsync(id);
			return View(student);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(Student viewModel)
		{
			try
			{
				var student = await dbContext.Students.FindAsync(viewModel.Id);
				if (student != null)
				{
					student.Name = viewModel.Name;
					student.Email = viewModel.Email;
					student.Phone = viewModel.Phone;
					student.Subscribed = viewModel.Subscribed;

					await dbContext.SaveChangesAsync();

					TempData["insert_message"] = $" Student -{student.Name} Edited..";
					logger.LogInformation("Student record updated..{studentNm} ({StudentEm})", student.Name, student.Email);
				}
				return RedirectToAction("List", "Students");
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error occurred while editing student: {Id}", viewModel.Id);
				return View(viewModel);
			}
		}

		// Delete by Object
		[HttpPost]
		public async Task<IActionResult> DeleteO(Student viewModel)
		{
			// Retrieve the student from the database without tracking
			var student = await dbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);
			if (student != null)
			{
				//// Attach the entity to the context so it can be tracked
				//dbContext.Students.Attach(student); // Attach the student entity to the context

				//// Remove the entity from the context (since it's now tracked)
				//dbContext.Students.Remove(student);  // Remove the student entity
				dbContext.Students.Remove(student);
				// Case 1: Using viewModel as the Entity
				// you can use the viewModel in place of student, but the key difference is whether the entity is tracked by the DbContext or not.
				// viewModel is an instance of the Student model that you’re passing into the action method. However, the viewModel is not automatically tracked by the DbContext, especially if you’re using AsNoTracking() when fetching the student from the database.
				//Case 2: Using viewModel without AsNoTracking()
				// If you fetch the entity from the database and don't use AsNoTracking(), then the entity returned will be tracked by the DbContext, and you can simply remove it.
				//However, if you're passing viewModel directly (maybe from a form), but that viewModel is not tracked by the context, then you still need to ensure it's attached to the DbContext before attempting to remove it.
				await dbContext.SaveChangesAsync();
				// Why Attach viewModel?
				//When you pass viewModel to the action, it is typically not being tracked unless it was fetched using the context directly(or if it was added to the context). In this case, you are passing the viewModel as a form of user input, and it may not be in the same state as an entity fetched by the DbContext.So, Attach() is necessary to let the DbContext know it should start tracking this entity for deletion.
			}
			TempData["insert_message"] = $" Deleted{viewModel.Name}...";
			// Redirect to the Students list page after deletion
			return RedirectToAction("List", "Students");
		}
		[HttpPost]
		public async Task<IActionResult> DeleteId(Guid Id)
		{
			try
			{
				var student = await dbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
				if (student != null)
				{
					dbContext.Students.Remove(student);
					await dbContext.SaveChangesAsync();

					logger.LogInformation("Student deleted: {StudentName} ({StudentEmail})", student.Name, student.Email);
					TempData["insert_message"] = $"Deleted -Name: {student.Name}..";

				}

				return RedirectToAction("List");
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error occurred while deleting student: {StudentId}", Id);
				return RedirectToAction("List");
			}
		}
		[HttpGet]
		public IActionResult CancelAction()
		{
			return RedirectToAction("List", "Students");
		}
	}
}
