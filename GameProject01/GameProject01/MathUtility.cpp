#include "MathUtility.h"
#include <cmath>

const float MathUtility::PI = 3.1415926535897932f; // 円周率

float MathUtility::sin(float degree)
{
	return (float)std::sin(radian(degree));
}

float MathUtility::cos(float degree)
{
	return (float)std::cos(radian(degree));
}

float MathUtility::tan(float degree)
{
	return (float)std::tan(radian(degree));
}

float MathUtility::asin(float x)
{
	return degree((float)std::asin(x));
}

float MathUtility::acos(float x)
{
	return degree((float)std::acos(x));
}

float MathUtility::atan(float y, float x)
{
	return degree((float)std::atan2(y, x));
}

float MathUtility::radian(float degree)
{
	return degree * (PI / 180.0f);
}
float MathUtility::degree(float radian)
{
	return radian * (180.0f / PI);
}

//ベクトルの向いている角度を求める(ベクトルから角度を計算する)
float MathUtility::angle(const GSvector2& v)
{
	return MathUtility::atan(v.y, v.x);
}

//ターゲット方向への角度を求める
float MathUtility::angle(const GSvector2& my, const GSvector2& target)
{
	return angle(target - my);
}

//角度の正規化(-360.0f～360.0f)
float MathUtility::normalizeAngle(float angle)
{
	return std::fmod(angle, 360.0f);
}

//角度差を求める
float MathUtility::subtractAngle(float my, float target)
{
	float d = normalizeAngle(target - my);
	return d;
}

//ターゲット方向への角度差を求める
float MathUtility::subtractAngle(const GSvector2& my, float dir, const GSvector2& target)
{
	return subtractAngle(dir, angle(my, target));
}

//線形補間関数
GSvector2 MathUtility::lerp(const GSvector2& start, const GSvector2& end, float t)
{
	if (t < 0.0f)
	{
		t = 0.0f;
	}
	else if (t > 1.0f)
	{
		t = 1.0f;
	}
	return start * (1.0f - t) + end * t;
}

//べき乗関数を使った補間関数
GSvector2 MathUtility::interpolatePower(const GSvector2& start, const GSvector2& end, float t, float power)
{
	return lerp(start, end, std::pow(t, power));
}

//べき乗関数と三角関数を使った補間関数
GSvector2 MathUtility::interpolateSinPower(const GSvector2& start, const GSvector2& end, float t, float power)
{
	return lerp(start, end, std::pow(MathUtility::sin(90 * t), power));
}