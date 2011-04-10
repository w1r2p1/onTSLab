﻿/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 07.04.2011
 * Time: 15:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace org.ontslab.trading.position {
	/// <summary>
	/// Description of Positions.
	/// </summary>
	public static class Positions {
		public static FixedSizePositionCalculator fixedSize(double size) {
			return new FixedSizePositionCalculator(size);
		}
		
		public static PercentOfBalanceBasedPositionCalculator percentOfBalance(
			double percent,
			double currentBalance,
			double maxLoss
		) {
			return new PercentOfBalanceBasedPositionCalculator(percent, currentBalance, maxLoss);
		}
		
		public static FloatingStopLoss floatingStopLoss(IList<double> based, double koeff) {
			return new FloatingStopLoss(based, koeff);
		}
		
		public static AbsoluteTakeProfit absoluteTakeProfit(double size) {
			return new AbsoluteTakeProfit(size);
		}
	}
}
