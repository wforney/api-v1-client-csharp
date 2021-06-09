namespace Info.Blockchain.API.Tests
{
	using System;
	using System.IO;
	using System.Reflection;

	using Newtonsoft.Json;

	/// <summary>
	/// The reflection utility class.
	/// </summary>
	public static class ReflectionUtil
	{
		/// <summary>
		/// Deserializes the file.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="customDeserialization">The custom deserialization.</param>
		/// <returns>System.Nullable&lt;T&gt;.</returns>
		/// <exception cref="Exception">Embedded resource with the name {resourceName} does not exist.</exception>
		public static T? DeserializeFile<T>(string fileName, Func<string, T>? customDeserialization = null)
		{
			var assembly = typeof(ReflectionUtil).GetTypeInfo().Assembly;

			var resourceName = $"Info.Blockchain.API.Tests.JsonObjects.{fileName}.json";

			using var stream = assembly.GetManifestResourceStream(resourceName);
			if (stream is null)
			{
				throw new Exception($"Embedded resource with the name {resourceName} does not exist.");
			}

			using var reader = new StreamReader(stream);
			var json = reader.ReadToEnd();
			return customDeserialization is null ? JsonConvert.DeserializeObject<T>(json) : customDeserialization(json);
		}
	}
}
