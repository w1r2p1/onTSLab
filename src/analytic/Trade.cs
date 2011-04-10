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
using TSLab.Script;

namespace org.ontslab.analytic
{
	/// <summary>
	/// Trivia representation of a closed(inactive) trade.
	/// </summary>
	public sealed class Trade {
		private Bar entry;
		private Bar exit;
		private double profit;
		
		public Trade(Bar entry, Bar exit, double profit) {
			this.entry = entry;
			this.exit = exit;
			this.profit = profit;
		}
		
		public Bar getEntry() {
			return entry;
		}
		
		public Bar getExit() {
			return exit;
		}
		
		public double getProfit() {
			return profit;
		}
	}
}
