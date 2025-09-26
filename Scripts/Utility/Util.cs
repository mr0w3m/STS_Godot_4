using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public class Util
{
	public static float MapValue(float a, float min, float max, float _min, float _max)
	{
		return (a - min) * (_max - _min) / (max - min) + _min;
	}
}