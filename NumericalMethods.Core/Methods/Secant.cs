﻿using System;
using NumericalMethods.Tests.Extensions;

namespace NumericalMethods.Core.Methods
{
	public class Secant
	{
		private readonly double _accuracy;
		private readonly Interval _interval;
		private readonly Func<double, double> _function;

		private int _iterationCounter; 
		
		public Secant(Func<double, double> function, double accuracy, Interval interval)
		{
			_function = function;
			_accuracy = accuracy;
			_interval = interval;
		}

		public int IterationCounter => _iterationCounter;

		public double Process()
		{
			Interval interval = new (_interval.A, _interval.B);

			if (!IntervalHasValue(interval))
				throw new Exception("There is no root on a given interval");

			while (interval.Length > _accuracy)
			{
				var intersection = CalculateLineIntersection(interval);

				if (IsRootOfEquation(intersection))
					break;
				
				_iterationCounter++;

				if (_function(interval.A) * _function(intersection) > 0)
				{
					interval.A = intersection;
				}
				else
				{
					interval.B = intersection;
				}
			}

			return interval.A;
		}
		
		private bool IntervalHasValue(Interval interval)
		{
			return _function(interval.A) * _function(interval.B) < 0;
		}

		private double CalculateLineIntersection(Interval interval)
		{
			return interval.A - _function(interval.A) * (interval.A - interval.B) /
				(_function(interval.A) - _function(interval.B));
		}
		
		private bool IsRootOfEquation(double argument)
		{
			return _function(argument).EqualWithTolerance(0, _accuracy);
		}
	}
}