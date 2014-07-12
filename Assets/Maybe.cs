using System;

public enum MaybeType { Just, Nothing };

public abstract class Maybe<T> {
	private readonly MaybeType tag;
	protected Maybe(MaybeType tag) {
		this.tag = tag;
	}
	public MaybeType Tag { get { return tag; } }

	public bool MatchNone() {
		return Tag == MaybeType.Nothing;
	}

	public bool MatchJust(out T value) {
		if (Tag == MaybeType.Just) {
			value = ((Just<T>)this).Value;
		} else {
			value = default(T);
		}
		return Tag == MaybeType.Just;
	}
}

public class Nothing<T> : Maybe<T> {
	public Nothing() : base(MaybeType.Nothing) { }

	public override string ToString ()
	{
		return string.Format ("Nothing");
	}
}

public class Just<T> : Maybe<T> {
	public Just(T value) : base(MaybeType.Just) {
		this.value = value;
	}
	private readonly T value;
	public T Value { get { return value; } }

	public override string ToString ()
	{
		return string.Format ("Just {0}", Value);
	}
}

public static class Maybe {
	public static Maybe<T> Nothing<T>() {
		return new Nothing<T>();
	}
	public static Maybe<T> Just<T>(T value) {
		return new Just<T>(value);
	}

	public static Maybe<R> Map<T, R>(this Maybe<T> input, Func<T, R> f) {
		T v;
		if(input.MatchJust(out v)) {
			return Maybe.Just(f(v));
		} else {
			return Maybe.Nothing<R>();
		}
	}

	public static Maybe<R> Bind<T, R>(this Maybe<T> input, Func<T, Maybe<R>> f) {
		T v;
		if(input.MatchJust(out v)) {
			return f(v);
		} else {
			return Maybe.Nothing<R>();
		}
	}

	public static Maybe<R> Select<S, R>(this Maybe<S> source, Func<S, R> selector) {
		return source.Map(selector);
	}

	public static Maybe<R> SelectMany<S, V, R> (this Maybe<S> source, Func<S, Maybe<V>> valueSelector, Func<S, V, R> resultSelector) {
		return source.Bind (sourceValue => valueSelector (sourceValue).Map (resultValue => resultSelector (sourceValue, resultValue)));
	}
}