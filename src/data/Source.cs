﻿/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 10.04.2011
 * Time: 18:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
/***************************************************************************
*   Copyright (C) 2011 by Denis M. Gabaydulin                             *
*                                                                         *
*   This program is free software; you can redistribute it and/or modify  *
*   it under the terms of the GNU Lesser General Public License as        *
*   published by the Free Software Foundation; either version 3 of the    *
*   License, or (at your option) any later version.                       *
*                                                                         *
***************************************************************************/

using TSLab.Script;

namespace org.ontslab.data {
	/// <summary>
	/// Description of SourceUtils.
	/// </summary>
	
	public static class SecurityExtensions {
        public static bool isLastTradeLoss(this ISecurity source) {
            IPosition lastPosition = source.Positions.LastPositionClosed;
			
			if (null == lastPosition)
				return false;
			
			return lastPosition.Profit() < 0;
        }
    }   
}
