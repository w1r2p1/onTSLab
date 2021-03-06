﻿/***************************************************************************
*   Copyright (C) 2011 by Denis M. Gabaydulin                             *
*                                                                         *
*   This program is free software; you can redistribute it and/or modify  *
*   it under the terms of the GNU Lesser General Public License as        *
*   published by the Free Software Foundation; either version 3 of the    *
*   License, or (at your option) any later version.                       *
*                                                                         *
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;

using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;
using TSLab.DataSource;

namespace org.ontslab.data {
	/// <summary>
	/// Description of DataHelpers.
	/// </summary>
	public static class DataHelpers {
		public static IList<double> roundList(IList<double> data, int precision) {
			return data.Select(
				elt => Math.Round(elt, precision) 
			).ToList();
		}

		public static IList<double> generateATR(IContext ctx, ISecurity source, int period) {
			return ctx.GetData(
				"ATR" + period.ToString(),
				new[] { period.ToString() }, delegate {
					return Series.AverageTrueRange(source.Bars, period);
				}
			);
		}

		public static IList<double> generateATR(IContext ctx, IReadOnlyList<IDataBar> data, int period) {
			return ctx.GetData(
				"ATR" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return Series.AverageTrueRange(data, period);
				}
			);
		}
		
		public static IList<double> generateADX(IContext ctx, ISecurity source, int period) {
			ADXFull generator = new ADXFull();
			generator.Context = ctx;
			generator.Period = period;
			
			return ctx.GetData(
				"ADX" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return generator.Execute(source);
				}
			);
		}
		
		public static IList<double> generateEMA(IContext ctx, ISecurity source, int period) {
			return generateEMA(ctx, source.ClosePrices, period);
		}
		
		public static IList<double> generateEMA(IContext ctx, IList<double> source, int period) {
			return ctx.GetData(
				"EMA" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return Series.EMA(source, period);
				}
			);
		}
		
		public static IList<double> generateSMA(IContext ctx, ISecurity source, int period) {
			return generateSMA(ctx, source.ClosePrices, period);
		}

		public static IList<double> generateSMA(IContext ctx, IList<double> source, int period, string cacheLabel) {
			return ctx.GetData(
				"SMA" + period.ToString(),
				new[] { cacheLabel }, delegate {
					return Series.SMA(source, period);
				}
			);
		}

		public static IList<double> generateSMA(IContext ctx, IList<double> source, int period) {
			return generateSMA(ctx, source, period, period.ToString());
		}
		
		public static IList<double> generateHighest(IContext ctx, ISecurity source, int period) {
			return generateHighest(ctx, source.HighPrices, period);
		}
		
		public static IList<double> generateHighest(IContext ctx, IList<double> source, int period) {
			return ctx.GetData(
				"Highest" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return Series.Highest(source, period);
				}
			);
		}
		
		public static IList<double> generateLowest(IContext ctx, ISecurity source, int period) {
			return generateLowest(ctx, source.LowPrices, period);
		}
		
		public static IList<double> generateLowest(IContext ctx, IList<double> source, int period) {
			return ctx.GetData(
				"Lowest" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return Series.Lowest(source, period);
				}
			);
		}
		
		public static IList<double> generateShift(IContext ctx, IList<double> source, int period) {
			return ctx.GetData(
				"Shift" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return Series.Shift(source, period);
				}
			);
		}

		public static IList<double> generateShift(IList<double> source, int period) {
			List<double> result = new List<double>();
			if (period < 0) {
				int absPeriod = Math.Abs(period);
				result = source.Skip(absPeriod).Take(source.Count - absPeriod).ToList();
				for (int i = 0; i < absPeriod; i++) {
					result.Add(0);
				}
			} else if(period > 0) {
				for (int i = 0; i < Math.Abs(period); i++) {
					result.Add(0);
				}
				result.AddRange(source.Skip(period).ToList());
			} else {
				return source;
			}

			return result;
		}

		public static IList<double> generateStDev(IContext ctx, IList<double> source, int period) {
			return ctx.GetData(
				"STDEV" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return Series.StDev(source, period);
				}
			);
		}
		
		public static IList<double> generateRSI(IContext ctx, IList<double> source, int period) {
			return ctx.GetData(
				"RSI" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return Series.CuttlerRSI(source, period);
				}
			);
		}
		
		public static double highest(int barIndex, IList<double> data, int period) {
			return data.
				Skip(barIndex).
				Take(Math.Min(period, data.Count)).
				Max();
		}
		
		public static double lowest(int barIndex, IList<double> data, int period) {
			return data.
				Skip(barIndex).
				Take(Math.Min(period, data.Count)).
				Min();
		}
		
		public static IList<double> generateSub(IContext ctx, IList<double> source, int period) {
			return ctx.GetData(
				"Diff" + period.ToString(),
				new[] {period.ToString()}, delegate {
					return Series.Sub(source, Series.Shift(source, period));
				}
			);
		}

		public static IList<double> generateMean(IList<double> one, IList<double> two, int period) {
			IList<double> result = new List<double>();

			for (int i = 0; i < one.Count; i++) {
				double sum = 0.0;
				for (int j = 0; j < period; j++) {
					if (i - j < 0) {
						sum += 0;
					} else {
						sum += one[i - j] * two[i - j];
					}
				}
				result.Add(sum / period);
			}

			return result;
		}
		
		public static IList<double> generateLn(IList<double> data) {
			return data.Select(elt => elt > 0 ? Math.Log(elt) : 0).ToList();
		}
		
		public static IList<double> generateDiv(IList<double> dividend, IList<double> divisor) {
			return dividend.Select((elt, index) => divisor[index] > 0 ? elt / divisor[index] : 0).ToList();
		}

		public static IList<double> generateMul(IList<double> data, IList<double> multiplier) {
			return data.Select((elt, index) => data[index] * multiplier[index]).ToList();
		}

		public static IList<double> generateSub(IList<double> one, IList<double> two) {
			return one.Select((elt, index) => elt - two[index]).ToList();
		}

		public static IList<double> generatePow(IList<double> data, double pow) {
			return data.Select(elt => elt > 0 ? Math.Pow(elt, pow) : 0).ToList();
		}

		public static IList<decimal> toDecimal(IList<double> data) {
			return data.Select(elt => (decimal) elt).ToList();
		}

		public static IList<double> generateVariance(IList<double> data, int period) {
			IList<double> result = new List<double>();

			for (int i = 0; i < data.Count; i++) {
				double squareSum = 0.0;
				double sum = 0.0;

				// calc average
				for (int j = 0; j < period; j++) {
					if (i - j < 0) {
						sum += 0;
					} else {
						sum += data[i - j];
					}
				}
				double avg = sum / period;

				for (int j = 0; j < period; j++) {
					if (i - j < 0) {
						squareSum += 0;
					} else {
						squareSum += Math.Pow(data[i - j] - avg, 2);
					}
				}

				result.Add(squareSum / period);
			}

			return result;
		}
	}
}
