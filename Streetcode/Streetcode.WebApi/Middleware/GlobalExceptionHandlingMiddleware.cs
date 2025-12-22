using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Streetcode.BLL.Exceptions.CustomExceptions;

namespace Streetcode.WebApi.Middleware
{
	/// <summary>
	/// Global middleware for handling exceptions and converting them into HTTP responses.
	/// </summary>
	public class GlobalExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="GlobalExceptionHandlingMiddleware"/> class.
		/// </summary>
		/// <param name="next">Next middleware in the pipeline.</param>
		/// <param name="logger">Logger instance.</param>
		public GlobalExceptionHandlingMiddleware(
			RequestDelegate next,
			ILogger<GlobalExceptionHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		/// <summary>
		/// Invokes the middleware and handles thrown exceptions.
		/// </summary>
		/// <param name="context">HTTP context.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (ValidationException ex)
			{
				await HandleValidationAsync(context, ex);
			}
			catch (Exception ex)
			{
				await HandleUnhandledAsync(context, ex);
			}
		}

		/// <summary>
		/// Handles validation exceptions.
		/// </summary>
		/// <param name="context">HTTP context.</param>
		/// <param name="exception">Validation exception.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		private static Task HandleValidationAsync(
			HttpContext context,
			ValidationException exception)
		{
			context.Response.StatusCode = StatusCodes.Status400BadRequest;
			context.Response.ContentType = "application/json";

			var response = new
			{
				statusCode = context.Response.StatusCode,
				message = "Validation failed",
				errors = exception.Errors.Select(e => new
				{
					field = e.Key,
					messages = e.Value
				})
			};

			return context.Response.WriteAsync(JsonSerializer.Serialize(response));
		}

		/// <summary>
		/// Handles unhandled exceptions.
		/// </summary>
		/// <param name="context">HTTP context.</param>
		/// <param name="exception">Unhandled exception.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		private Task HandleUnhandledAsync(
			HttpContext context,
			Exception exception)
		{
			_logger.LogError(exception, "Unhandled exception");

			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			context.Response.ContentType = "application/json";

			var response = new
			{
				statusCode = context.Response.StatusCode,
				message = "Internal server error"
			};

			return context.Response.WriteAsync(JsonSerializer.Serialize(response));
		}
	}
}
