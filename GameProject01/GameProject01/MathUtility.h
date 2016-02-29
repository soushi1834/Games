#ifndef _MATH_UTILITY_H_
#define _MATH_UTILITY_H_

#include <gslib.h>

class MathUtility
{
public:
	static const float PI; // 円周率
	static float sin(float degree); // サイン
	static float cos(float degree); // コサイン
	static float tan(float degree); // タンジェント
	static float asin(float x); // 逆サイン（サイン値から角度を求める）
	static float acos(float x); // 逆コサイン（コサイン値から角度を求める）
	static float atan(float y, float x); // 逆タンジェント（タンジェント値から角度を求める）
	static float radian(float degree); // 角度をラジアンに変換
	static float degree(float radian); // ラジアンを角度に変換
	static float angle(const GSvector2& v); //ベクトルの向いている角度を求める(ベクトルから角度を計算する)
	static float angle(const GSvector2& my, const GSvector2& target); //ターゲット方向への角度を求める
	static float normalizeAngle(float angle); //角度の正規化(-360.0f～360.0f)
	static float subtractAngle(float my, float target); //角度差を求める
	static float subtractAngle(const GSvector2& my, float dir, const GSvector2& target); //ターゲット方向への角度差を求める
	static GSvector2 lerp(const GSvector2& start, const GSvector2& end, float t); //線形補間関数
	static GSvector2 interpolatePower(const GSvector2& start, const GSvector2& end, float t, float power); //べき乗関数を使った補間関数
	static GSvector2 interpolateSinPower(const GSvector2& start, const GSvector2& end, float t, float power); //べき乗関数と三角関数を使った補間関数
};

#endif