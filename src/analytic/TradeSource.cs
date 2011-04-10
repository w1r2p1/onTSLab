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
using TSLab.Script;

namespace org.ontslab.analytic
{
	/// <summary>
	/// Description of TradeSource.
	/// </summary>
	public class TradeSource {
		private ISecurity source;
		private List<Trade> trades = new List<Trade>();
		private bool fetched = false;
		
		public TradeSource(ISecurity source) { this.source = source; }
		
		public ISecurity getBaseSource() {
			return source;
		}
		
		public List<Trade> getTrades() {
			if (!fetched) {
				fetch();
			}
			
			return trades;
		}
		
		public IDictionary<int, Trade> getBarIndexedTrades() {
			if (!fetched) {
				fetch();
			}
			
			IDictionary<int, Trade> barIndexedTrades = new Dictionary<int, Trade>();
			
			trades.ForEach(trade => barIndexedTrades.Add(
					source.Bars.IndexOf(trade.getEntry()),
					trade
				)
			);
			
			return barIndexedTrades;
		}
		
		private void fetch() {
			IEnumerator<IPosition> positionEnum = source.Positions.GetEnumerator();
			
			while (positionEnum.MoveNext()) {
				if (!positionEnum.Current.IsActive) {
					Trade trade = new Trade(
						positionEnum.Current.EntryBar,
						positionEnum.Current.ExitBar,
						positionEnum.Current.Profit()
					);
					
					trades.Add(trade);
				}
			}
			
			fetched = true;	
		}
	}
}
