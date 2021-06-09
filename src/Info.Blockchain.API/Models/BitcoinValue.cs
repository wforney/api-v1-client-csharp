namespace Info.Blockchain.API.Models
{
	using System;
	using System.Globalization;

	/// <summary>
	/// The bitcoin value class. Implements the <see cref="IEquatable{BitcoinValue}" />.
	/// </summary>
	/// <seealso cref="IEquatable{BitcoinValue}" />
	public class BitcoinValue : IEquatable<BitcoinValue>
	{
		/// <summary>
		/// The bits per bitcoin
		/// </summary>
		private const int BITS_PER_BITCOIN = 1000000;

		/// <summary>
		/// The millibits per bitcoin
		/// </summary>
		private const int MILLIBITS_PER_BITCOIN = 1000;

		/// <summary>
		/// The satoshis per bitcoin
		/// </summary>
		private const int SATOSHIS_PER_BITCOIN = 100000000;

		/// <summary>
		/// The BTC
		/// </summary>
		private readonly decimal btc;

		/// <summary>
		/// Initializes a new instance of the <see cref="BitcoinValue" /> class.
		/// </summary>
		/// <param name="btc">The BTC.</param>
		public BitcoinValue(decimal btc) => this.btc = btc;

		/// <summary>
		/// Gets the zero.
		/// </summary>
		/// <value>The zero.</value>
		public static BitcoinValue Zero => new(0);

		/// <summary>
		/// Gets the bits.
		/// </summary>
		/// <value>The bits.</value>
		public decimal Bits => this.btc * BITS_PER_BITCOIN;

		/// <summary>
		/// Gets the milli bits.
		/// </summary>
		/// <value>The milli bits.</value>
		public decimal MilliBits => this.btc * MILLIBITS_PER_BITCOIN;

		/// <summary>
		/// Gets the satoshis.
		/// </summary>
		/// <value>The satoshis.</value>
		public long Satoshis => (long)(this.btc * SATOSHIS_PER_BITCOIN);

		/// <summary>
		/// Froms the bits.
		/// </summary>
		/// <param name="bits">The bits.</param>
		/// <returns>BitcoinValue.</returns>
		public static BitcoinValue FromBits(decimal bits) => new(bits / BITS_PER_BITCOIN);

		/// <summary>
		/// Froms the BTC.
		/// </summary>
		/// <param name="btc">The BTC.</param>
		/// <returns>BitcoinValue.</returns>
		public static BitcoinValue FromBtc(decimal btc) => new(btc);

		/// <summary>
		/// Froms the milli bits.
		/// </summary>
		/// <param name="mBtc">The m BTC.</param>
		/// <returns>BitcoinValue.</returns>
		public static BitcoinValue FromMilliBits(decimal mBtc) => new(mBtc / MILLIBITS_PER_BITCOIN);

		/// <summary>
		/// Froms the satoshis.
		/// </summary>
		/// <param name="satoshis">The satoshis.</param>
		/// <returns>BitcoinValue.</returns>
		public static BitcoinValue FromSatoshis(long satoshis) => new((decimal)satoshis / SATOSHIS_PER_BITCOIN);

		/// <summary>
		/// Implements the - operator.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <returns>The result of the operator.</returns>
		public static BitcoinValue operator -(BitcoinValue x, BitcoinValue y)
		{
			var btc = x.btc - y.btc;
			return new BitcoinValue(btc);
		}

		/// <summary>
		/// Implements the + operator.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <returns>The result of the operator.</returns>
		public static BitcoinValue operator +(BitcoinValue x, BitcoinValue y)
		{
			var btc = x.btc + y.btc;
			return new BitcoinValue(btc);
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// <see langword="true" /> if the current object is equal to the <paramref name="other" />
		/// parameter; otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(BitcoinValue? other) => this.btc == other?.btc;

		/// <inheritdoc />
		public override bool Equals(object? obj) => obj is BitcoinValue bv && this.Equals(bv);

		/// <summary>
		/// Gets the BTC.
		/// </summary>
		/// <returns>System.Decimal.</returns>
		public decimal GetBtc() => this.btc;

		/// <inheritdoc />
		public override int GetHashCode() => this.btc.GetHashCode();

		/// <inheritdoc />
		public override string ToString() => this.btc.ToString(CultureInfo.CurrentCulture);
	}
}
