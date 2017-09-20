using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
	/// <summary>
	/// Provide static utility extension classes operating on IEnumerable interface
	/// </summary>
	public static class IEnumerableExtension
	{

		/// <summary>
		/// Breaks an enumeration of items into chunks of a specific size. Every resulting chunk is of required size except for the last one which contains remaining elements (but is always lesser or equal than input chunksize).
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">Source enumerable. Can be null.</param>
		/// <param name="chunksize">Required chunk size. Must be bigger than zero.</param>
		/// <returns>Enumeration of enumerables which represents the chunks.</returns>
		/// <exception cref="ArgumentException">When the chunk size is less or equal to zero.</exception>
		/// <exception cref="ArgumentNullException">When input enumerable is null.</exception>
		public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize)
		{
			if (chunksize <= 0)
				throw new ArgumentException("Chunk size must be bigger than zero.", "chunksize");

			if (source == null)
				throw new ArgumentNullException("source");

			while (source.Any())
			{
				yield return source.Take(chunksize);
				source = source.Skip(chunksize);
			}
		}


		/// <summary>
		/// Creates cartesian product from given sequeces (all possible combinations from given items).
		/// </summary>
		/// <typeparam name="T">Type of items in sequences.</typeparam>
		/// <param name="sequences">Sequences to process.</param>
		/// <returns></returns>
		/// <remarks>Taken from Eric Lippert (http://blogs.msdn.com/b/ericlippert/archive/2010/06/28/computing-a-cartesian-product-with-linq.aspx)</remarks>
		/// <example>
		/// from input { { A, B}, { 1, 2, 3}, { Z } }
		/// the output will be { { A, 1, Z }, { A, 2, Z }, { A, 3, Z }, { B, 1, Z }, { B, 2, Z }, { B, 3, Z } }
		/// </example>
		public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
		{
			IEnumerable<IEnumerable<T>> emptyProducts = new[] { Enumerable.Empty<T>() };
			return sequences.Aggregate(emptyProducts, (accumulator, sequence) =>
				from accumulatedSequence in accumulator
				from item in sequence
				select accumulatedSequence.Concat(new[] { item }));
		}

		/// <summary>
		/// Throws ArgumentNullExcpetion when input argument is null or ArgumentException when input argument contains a null element.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumerable"></param>
		/// <param name="parameterName"></param>
		//public static void ThrowIfNullOrNullElements<T>(this IEnumerable<T> enumerable, string parameterName)
		//{
		//	// throw argument null exception if null
		//	enumerable.ThrowIfNull(parameterName);
		//	// throw argument exception when any of the element is null
		//	enumerable.ThrowIfNullElement(parameterName);
		//}

		/// <summary>
		/// Throws ArgumentException when any of the elements of the collection is null.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumerable"></param>
		/// <param name="parameterName"></param>
		public static void ThrowIfNullElement<T>(this IEnumerable<T> enumerable, string parameterName)
		{
			if (enumerable == null)
				return;
			if (enumerable.Any(item => item == null))
				throw new ArgumentException("Input parameter cannot contain null element.", parameterName);
		}

		/// <summary>
		/// Projects each element of a sequence into a new form.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <typeparam name="TResult">The type of the value returned by selector.</typeparam>
		/// <param name="source">A sequence of values to invoke a transform function on; it can be null.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <returns>
		/// An <see cref="System.Collections.Generic.IEnumerable&lt;T&gt;"/> whose elements are the result of invoking the transform function on each element of source
		/// or null if <paramref name="source"/> is null.
		/// </returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="selector"/> is null.</exception>
		public static IEnumerable<TResult> SelectEx<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			return source == null ? null : source.Select(selector);
		}

		/// <summary>
		/// Splits the <paramref name="source"/> into two parts by specific <paramref name="condition"/>.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source">A sequence of values to invoke a transform function on.</param>
		/// <param name="condition">A condition to split <paramref name="source"/> to 'true' part and 'false' part.</param>
		/// <returns>A pair of cloned sequences; 1st is the true part and 2nd the false part of <paramref name="source"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="condition"/> is null.</exception>
		/// <exception cref="NullReferenceException"><paramref name="source"/> is null.</exception>
		//public static Tuple<IEnumerable<T>, IEnumerable<T>> Split<T>(this IEnumerable<T> source, Func<T, bool> condition)
		//{
		//	condition.ThrowIfNull("condition");

		//	var truePart = new List<T>();
		//	var falsePart = new List<T>();
		//	var result = new Tuple<IEnumerable<T>, IEnumerable<T>>(truePart, falsePart);

		//	foreach (var item in source)
		//		if (condition(item)) truePart.Add(item);
		//		else falsePart.Add(item);

		//	return result;
		//}

		/// <summary>
		/// Splits the <paramref name="source"/> into two parts by specific <paramref name="condition"/>.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source">A sequence of values to invoke a transform function on; it can be null.</param>
		/// <param name="condition">A condition to split <paramref name="source"/> to 'true' part and 'false' part.</param>
		/// <returns>A pair of sequences or null if <paramref name="source"/> is null; 1st is the true part and 2nd the false part of <paramref name="source"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="condition"/> is null.</exception>
		//public static Tuple<IEnumerable<T>, IEnumerable<T>> SplitEx<T>(this IEnumerable<T> source, Func<T, bool> condition)
		//{
		//	if (source == null) return null;

		//	return source.Split<T>(condition);
		//}

		/// <summary>
		/// Converts given enumerable to array of items of same type.
		/// Also works when input argument is null in which case returns empty array.
		/// </summary>
		/// <typeparam name="T">Type of agument.</typeparam>
		/// <param name="enumerable">Input sequence.</param>
		/// <returns>Array of items, never null.</returns>
		public static T[] ToArrayEx<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null)
				return new T[0];

			return enumerable.ToArray();
		}

		/// <summary>
		/// Converts given enumerable to array of items of same type.
		/// Also works when input argument is null in which case returns <paramref name="nullValue"/>.
		/// </summary>
		/// <typeparam name="T">Type of agument.</typeparam>
		/// <param name="enumerable">Input sequence.</param>
		/// <param name="nullValue">A value to return if <paramref name="enumerable"/> is null.</param>
		/// <returns>Array of items or <paramref name="nullValue"/> if <paramref name="enumerable"/> is null.</returns>
		public static T[] ToArrayEx<T>(this IEnumerable<T> enumerable, T[] nullValue)
		{
			if (enumerable == null)
				return nullValue;

			return enumerable.ToArray();
		}

		/// <summary>
		/// Creates a <see cref="System.Collections.Generic.Dictionary&lt;TKey,TValue&gt;"/> from an <see cref="System.Collections.Generic.IEnumerable&lt;T&gt;"/> according to a specified key selector function.
		/// Also works when input argument is null in which case returns null.
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TSource"></typeparam>
		/// <param name="source"></param>
		/// <param name="keySelector"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		public static Dictionary<TKey, TSource> ToDictionaryEx<TKey, TSource>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			return source == null ? null : source.ToDictionary(keySelector, comparer);
		}

		/// <summary>
		/// Creates a <see cref="System.Collections.Generic.Dictionary&lt;TKey,TValue&gt;"/> from an <see cref="System.Collections.Generic.IEnumerable&lt;T&gt;"/> according to a specified key selector function.
		/// Also works when input argument is null in which case returns null.
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TElement"></typeparam>
		/// <param name="source"></param>
		/// <param name="keySelector"></param>
		/// <param name="elementSelector"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		public static Dictionary<TKey, TElement> ToDictionaryEx<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			return source == null ? null : source.ToDictionary(keySelector, elementSelector, comparer);
		}

		/// <summary>
		/// Determines whether the sequence contains any item.
		/// Also works when input argument is null in which case returns false.
		/// </summary>
		/// <typeparam name="T">Type of agument.</typeparam>
		/// <param name="enumerable">Input sequence.</param>
		/// <returns>True if the input sequence contains any item; otherwise false.</returns>
		public static bool AnyEx<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null)
				return false;

			return enumerable.Any();
		}

		#region FirstOrDefault

		/// <summary>
		/// Returns the first element of a sequence, or a default value if the sequence contains no elements.
		/// Also works when input argument is null in which case returns false.
		/// </summary>
		/// <typeparam name="T">Type of agument.</typeparam>
		/// <param name="enumerable">Input sequence.</param>
		/// <returns>First element of a sequence, or a default value if the sequence contains no elements or it is null.</returns>
		public static T FirstOrDefaultEx<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null)
				return default(T);

			return enumerable.FirstOrDefault();
		}

		/// <summary>
		/// Returns the first element of a sequence, or a default value if the sequence contains no elements.
		/// Also works when input argument is null in which case returns false.
		/// </summary>
		/// <typeparam name="T">Type of agument.</typeparam>
		/// <param name="enumerable">Input sequence.</param>
		/// <param name="predicate">Predicate.</param>
		/// <returns>First element of a sequence, or a default value if the sequence contains no elements or it is null.</returns>
		public static T FirstOrDefaultEx<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
		{
			if (enumerable == null)
				return default(T);

			return enumerable.FirstOrDefault(predicate);
		}

		/// <summary>
		/// Returns the first element of a sequence, or specific default value if the sequence contains no elements.
		/// </summary>
		/// <typeparam name="T">Type of agument.</typeparam>
		/// <param name="enumerable">Input sequence.</param>
		/// <param name="defaultValue">A default value to use if the sequence contains no elements.</param>
		/// <returns>First element of a sequence, or <paramref name="defaultValue"/> if the sequence contains no elements.</returns>
		public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue)
		{
			if (!enumerable.Any())
				return defaultValue;

			return enumerable.First();
		}

		/// <summary>
		/// Returns the first element of a sequence, or specific default value if the sequence contains no elements.
		/// Also works when input argument is null in which case returns false.
		/// </summary>
		/// <typeparam name="T">Type of agument.</typeparam>
		/// <param name="enumerable">Input sequence.</param>
		/// <param name="defaultValue"></param>
		/// <returns>First element of a sequence, or <paramref name="defaultValue"/> if the sequence contains no elements or it is null.</returns>
		public static T FirstOrDefaultEx<T>(this IEnumerable<T> enumerable, T defaultValue)
		{
			if (enumerable == null)
				return defaultValue;

			return enumerable.FirstOrDefault(defaultValue);
		}

		/// <summary>
		/// Returns the first element of a sequence, or default value constructed if the sequence contains no elements.
		/// </summary>
		/// <typeparam name="T">Type of agument.</typeparam>
		/// <param name="enumerable">Input sequence.</param>
		/// <param name="defaultValueFactory">A function that constructs default value to use if the sequence contains no elements.</param>
		/// <returns>First element of a sequence, or a default value constructed by <paramref name="defaultValueFactory"/> if the sequence contains no elements.</returns>
		public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, Func<T> defaultValueFactory)
		{
			if (!enumerable.Any())
				return defaultValueFactory();

			return enumerable.First();
		}

		/// <summary>
		/// Returns the first element of a sequence, or default value constructed if the sequence contains no elements.
		/// Also works when input argument is null in which case returns false.
		/// </summary>
		/// <typeparam name="T">Type of agument.</typeparam>
		/// <param name="enumerable">Input sequence.</param>
		/// <param name="defaultValueFactory">A function that constructs default value to use if the sequence contains no elements.</param>
		/// <returns>First element of a sequence, or a default value constructed by <paramref name="defaultValueFactory"/> if the sequence contains no elements or it is null.</returns>
		public static T FirstOrDefaultEx<T>(this IEnumerable<T> enumerable, Func<T> defaultValueFactory)
		{
			if (enumerable == null)
				return defaultValueFactory();

			return enumerable.FirstOrDefault(defaultValueFactory);
		}

		#endregion

		#region LastOrDefault

		/// <summary>
		/// Returns the last element of a sequence, or a default value if the sequence contains no elements.
		/// Also works when input argument is null in which case returns false.
		/// </summary>
		/// <typeparam name="T">Type of agument.</typeparam>
		/// <param name="enumerable">Input sequence.</param>
		/// <returns>Last element of a sequence, or a default value if the sequence contains no elements or it is null.</returns>
		public static T LastOrDefaultEx<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null)
				return default(T);

			return enumerable.LastOrDefault();
		}

		/// <summary>
		/// Returns the last element of a sequence, or specific default value if the sequence contains no elements.
		/// </summary>
		/// <typeparam name="T">Type of agument.</typeparam>
		/// <param name="enumerable">Input sequence.</param>
		/// <param name="defaultValue">A default value to use if the sequence contains no elements.</param>
		/// <returns>Last element of a sequence, or <paramref name="defaultValue"/> if the sequence contains no elements.</returns>
		public static T LastOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue)
		{
			if (!enumerable.Any())
				return defaultValue;

			return enumerable.Last();
		}

		/// <summary>
		/// Returns the last element of a sequence, or specific default value if the sequence contains no elements.
		/// Also works when input argument is null in which case returns false.
		/// </summary>
		/// <typeparam name="T">Type of agument.</typeparam>
		/// <param name="enumerable">Input sequence.</param>
		/// <param name="defaultValue"></param>
		/// <returns>Last element of a sequence, or <paramref name="defaultValue"/> if the sequence contains no elements or it is null.</returns>
		public static T LastOrDefaultEx<T>(this IEnumerable<T> enumerable, T defaultValue)
		{
			if (enumerable == null)
				return defaultValue;

			return enumerable.LastOrDefault(defaultValue);
		}

		/// <summary>
		/// Returns the last element of a sequence, or default value constructed if the sequence contains no elements.
		/// </summary>
		/// <typeparam name="T">Type of agument.</typeparam>
		/// <param name="enumerable">Input sequence.</param>
		/// <param name="defaultValueFactory">A function that constructs default value to use if the sequence contains no elements.</param>
		/// <returns>Last element of a sequence, or a default value constructed by <paramref name="defaultValueFactory"/> if the sequence contains no elements.</returns>
		public static T LastOrDefault<T>(this IEnumerable<T> enumerable, Func<T> defaultValueFactory)
		{
			if (!enumerable.Any())
				return defaultValueFactory();

			return enumerable.Last();
		}

		/// <summary>
		/// Returns the last element of a sequence, or default value constructed if the sequence contains no elements.
		/// Also works when input argument is null in which case returns false.
		/// </summary>
		/// <typeparam name="T">Type of agument.</typeparam>
		/// <param name="enumerable">Input sequence.</param>
		/// <param name="defaultValueFactory">A function that constructs default value to use if the sequence contains no elements.</param>
		/// <returns>Last element of a sequence, or a default value constructed by <paramref name="defaultValueFactory"/> if the sequence contains no elements or it is null.</returns>
		public static T LastOrDefaultEx<T>(this IEnumerable<T> enumerable, Func<T> defaultValueFactory)
		{
			if (enumerable == null)
				return defaultValueFactory();

			return enumerable.LastOrDefault(defaultValueFactory);
		}

		#endregion

		/// <summary>
		/// Returns an empty enumeration if the given <c>enumeration</c> is null.
		/// </summary>
		/// <typeparam name="T">Type of agument.</typeparam>
		/// <param name="enumerable">Input sequence.</param>
		/// <returns>An empty enumeration if <c>enumeration</c> is null; <c>enumeration</c> otherwise.</returns>
		public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> enumerable)
		{
			return (enumerable == null) ? Enumerable.Empty<T>() : enumerable;
		}

		/// <summary>
		/// Selects all non-null items from the given <c>enumeration</c>.
		/// </summary>
		/// <typeparam name="T">Type of agument.</typeparam>
		/// <param name="enumerable">Input sequence.</param>
		/// <returns>All non-null items of the given <c>enumeration</c> if it is not null; an empty enumeration otherwise.</returns>
		public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null) yield break;

			foreach (var item in enumerable)
				if (item != null) yield return item;
		}

		/// <summary>
		/// Selects all non-null and non-empty strings from the given <c>enumeration</c>.
		/// </summary>
		/// <param name="enumerable">Input sequence.</param>
		/// <returns>All non-null and non-empty strings of the given <c>enumeration</c> if it is not null; an empty enumeration otherwise.</returns>
		public static IEnumerable<string> WhereNotNullOrEmpty(this IEnumerable<string> enumerable)
		{
			if (enumerable == null) yield break;

			foreach (var item in enumerable)
				if (!string.IsNullOrEmpty(item)) yield return item;
		}

		/// <summary>
		/// Selects all non-null and non-whitespace strings from the given <c>enumeration</c>.
		/// </summary>
		/// <param name="enumerable">Input sequence.</param>
		/// <returns>All non-null and non-whitespace strings of the given <c>enumeration</c> if it is not null; an empty enumeration otherwise.</returns>
		public static IEnumerable<string> WhereNotNullOrWhitespace(this IEnumerable<string> enumerable)
		{
			if (enumerable == null) yield break;

			foreach (var item in enumerable)
				if (!string.IsNullOrWhiteSpace(item)) yield return item;
		}

		#region Join

		/// <summary>
		/// Creates a string from <paramref name="enumerable"/> elements.
		/// </summary>
		/// <typeparam name="T">Element type.</typeparam>
		/// <param name="enumerable"><see cref="IEnumerable&lt;T&gt;"/> to join.</param>
		/// <param name="separator">If null, the value of <see cref="System.Globalization.TextInfo.ListSeparator"/> of <see cref="System.Globalization.CultureInfo.CurrentUICulture"/> is used.</param>
		/// <param name="appendSpace">Determines whether to append a space after <paramref name="separator"/> or not. If null, it is appended for non-primitive type values of <typeparamref name="T"/>.</param>
		/// <returns>A concatenation of <paramref name="enumerable"/> elements.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="enumerable"/> is null.</exception>
		public static string Join<T>(this IEnumerable<T> enumerable, string separator = null, bool? appendSpace = null)
		{
			return string.Join<T>(GetSeparator<T>(separator, appendSpace), enumerable);
		}

		/// <param name="separator">If null, the value of <see cref="System.Globalization.TextInfo.ListSeparator"/> of <see cref="System.Globalization.CultureInfo.CurrentUICulture"/> is used.</param>
		/// <param name="appendSpace">Determines whether to append a space after <paramref name="separator"/> or not. If null, it is appended for non-primitive types of <typeparamref name="T"/>.</param>
		private static string GetSeparator<T>(string separator, bool? appendSpace)
		{
			return separator ?? System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ListSeparator + (appendSpace ?? typeof(T).IsPrimitive ? " " : string.Empty);
		}

		/// <summary>
		/// Creates a string from <paramref name="enumerable"/> elements.
		/// </summary>
		/// <typeparam name="T">Element type.</typeparam>
		/// <param name="enumerable"><see cref="IEnumerable&lt;T&gt;"/> to join.</param>
		/// <param name="stringFactory">A function to get element string value.</param>
		/// <param name="separator">If null, the value of <see cref="System.Globalization.TextInfo.ListSeparator"/> of <see cref="System.Globalization.CultureInfo.CurrentUICulture"/> is used.</param>
		/// <param name="appendSpace">Determines whether to append a space after <paramref name="separator"/> or not. If null, it is appended for non-primitive type values of <typeparamref name="T"/>.</param>
		/// <returns>A concatenation of <paramref name="enumerable"/> elements.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="enumerable"/> is null.</exception>
		public static string Join<T>(this IEnumerable<T> enumerable, Func<T, string> stringFactory, string separator = null, bool? appendSpace = null)
		{
			return string.Join(GetSeparator<T>(separator, appendSpace), enumerable.Select(stringFactory));
		}

		/// <summary>
		/// Creates a string from <paramref name="enumerable"/> elements that can be null.
		/// </summary>
		/// <typeparam name="T">Element type.</typeparam>
		/// <param name="enumerable"><see cref="IEnumerable&lt;T&gt;"/> to join.</param>
		/// <param name="separator">If null, the value of <see cref="System.Globalization.TextInfo.ListSeparator"/> of <see cref="System.Globalization.CultureInfo.CurrentUICulture"/> is used.</param>
		/// <param name="appendSpace">Determines whether to append a space after <paramref name="separator"/> or not. If null, it is appended for non-primitive type values of <typeparamref name="T"/>.</param>
		/// <returns>A concatenation of <paramref name="enumerable"/> elements or null if it is null.</returns>
		public static string JoinEx<T>(this IEnumerable<T> enumerable, string separator = null, bool? appendSpace = null)
		{
			return enumerable == null ? null : enumerable.Join(separator, appendSpace);
		}


		/// <summary>
		/// Creates a string from <paramref name="enumerable"/> elements that can be null..
		/// </summary>
		/// <typeparam name="T">Element type.</typeparam>
		/// <param name="enumerable"><see cref="IEnumerable&lt;T&gt;"/> to join.</param>
		/// <param name="separator">If null, the value of <see cref="System.Globalization.TextInfo.ListSeparator"/> of <see cref="System.Globalization.CultureInfo.CurrentUICulture"/> is used.</param>
		/// <param name="appendSpace">Determines whether to append a space after <paramref name="separator"/> or not. If null, it is appended for non-primitive type values of <typeparamref name="T"/>.</param>
		/// <param name="stringFactory">A function to get element string value.</param>
		/// <returns>A concatenation of <paramref name="enumerable"/> elements or null if it is null.</returns>
		public static string JoinEx<T>(this IEnumerable<T> enumerable, Func<T, string> stringFactory, string separator = null, bool? appendSpace = null)
		{
			return enumerable == null ? null : enumerable.Join(stringFactory, separator, appendSpace);
		}

		#endregion

		#region KeyedCollection

		/// <summary>
		/// Creates a <see cref="System.Collections.ObjectModel.KeyedCollection&lt;TKey, TItem&gt;"/> of <paramref name="source"/> based on specified <paramref name="keySelector"/>.
		/// </summary>
		/// <typeparam name="TKey"><see cref="System.Collections.ObjectModel.KeyedCollection&lt;TKey, TItem&gt;"/></typeparam>
		/// <typeparam name="TItem"><see cref="System.Collections.ObjectModel.KeyedCollection&lt;TKey, TItem&gt;"/></typeparam>
		/// <param name="source">Input sequence.</param>
		/// <param name="keySelector">A method that provides a key of the specific item.</param>
		/// <returns>A <see cref="System.Collections.ObjectModel.KeyedCollection&lt;TKey, TItem&gt;"/> of <paramref name="source"/> based on specified <paramref name="keySelector"/>.</returns>
		//public static System.Collections.ObjectModel.KeyedCollection<TKey, TItem> ToKeyedCollection<TKey, TItem>(this IEnumerable<TItem> source, Func<TItem, TKey> keySelector)
		//{
		//	return new CADTeam.Common.Collections.KeyedCollectionEx<TKey, TItem>(keySelector, source);
		//}

		/// <summary>
		/// Creates a <see cref="System.Collections.ObjectModel.KeyedCollection&lt;TKey, TItem&gt;"/> of <paramref name="source"/> based on specified <paramref name="keySelector"/>.
		/// </summary>
		/// <typeparam name="TKey"><see cref="System.Collections.ObjectModel.KeyedCollection&lt;TKey, TItem&gt;"/></typeparam>
		/// <typeparam name="TItem"><see cref="System.Collections.ObjectModel.KeyedCollection&lt;TKey, TItem&gt;"/></typeparam>
		/// <param name="source">Input sequence.</param>
		/// <param name="keySelector">A method that provides a key of the specific item.</param>
		/// <param name="comparer"><see cref="System.Collections.ObjectModel.KeyedCollection&lt;TKey, TItem&gt;"/></param>
		/// <returns>A <see cref="System.Collections.ObjectModel.KeyedCollection&lt;TKey, TItem&gt;"/> of <paramref name="source"/> based on specified <paramref name="keySelector"/>.</returns>
		//public static System.Collections.ObjectModel.KeyedCollection<TKey, TItem> ToKeyedCollection<TKey, TItem>(this IEnumerable<TItem> source, Func<TItem, TKey> keySelector, IEqualityComparer<TKey> comparer)
		//{
		//	return new CADTeam.Common.Collections.KeyedCollectionEx<TKey, TItem>(keySelector, source, comparer);
		//}

		/// <summary>
		/// Creates a <see cref="System.Collections.ObjectModel.KeyedCollection&lt;TKey, TItem&gt;"/> of <paramref name="source"/> based on specified <paramref name="keySelector"/>.
		/// </summary>
		/// <typeparam name="TKey"><see cref="System.Collections.ObjectModel.KeyedCollection&lt;TKey, TItem&gt;"/></typeparam>
		/// <typeparam name="TItem"><see cref="System.Collections.ObjectModel.KeyedCollection&lt;TKey, TItem&gt;"/></typeparam>
		/// <param name="source">Input sequence.</param>
		/// <param name="keySelector">A method that provides a key of the specific item.</param>
		/// <param name="comparer"><see cref="System.Collections.ObjectModel.KeyedCollection&lt;TKey, TItem&gt;"/></param>
		/// <param name="dictionaryCreationThreshold"><see cref="System.Collections.ObjectModel.KeyedCollection&lt;TKey, TItem&gt;"/></param>
		/// <returns>A <see cref="System.Collections.ObjectModel.KeyedCollection&lt;TKey, TItem&gt;"/> of <paramref name="source"/> based on specified <paramref name="keySelector"/>.</returns>
		//public static System.Collections.ObjectModel.KeyedCollection<TKey, TItem> ToKeyedCollection<TKey, TItem>(this IEnumerable<TItem> source, Func<TItem, TKey> keySelector, IEqualityComparer<TKey> comparer, int dictionaryCreationThreshold)
		//{
		//	return new CADTeam.Common.Collections.KeyedCollectionEx<TKey, TItem>(keySelector, source, comparer, dictionaryCreationThreshold);
		//}

		#endregion

		#region Dictionary

		/// <summary>
		/// Creates a <see cref="System.Collections.Generic.Dictionary&lt;TKey,TValue&gt;"/> from an <see cref="System.Collections.Generic.IEnumerable&lt;T&gt;"/> according to a specified key selector function.
		/// Also works when input argument is null in which case returns null.
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TSource"></typeparam>
		/// <param name="source"></param>
		/// <param name="keySelector"></param>
		/// <returns></returns>
		public static Dictionary<TKey, TSource> ToDictionaryEx<TKey, TSource>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source == null ? null : source.ToDictionary(keySelector);
		}

		/// <summary>
		/// Creates a <see cref="System.Collections.Generic.Dictionary&lt;TKey,TValue&gt;"/> from an <see cref="System.Collections.Generic.IEnumerable&lt;T&gt;"/> according to a specified key selector function.
		/// Also works when input argument is null in which case returns null.
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TElement"></typeparam>
		/// <param name="source"></param>
		/// <param name="keySelector"></param>
		/// <param name="elementSelector"></param>
		/// <returns></returns>
		public static Dictionary<TKey, TElement> ToDictionaryEx<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source == null ? null : source.ToDictionary(keySelector, elementSelector);
		}

		#endregion
	}
}
