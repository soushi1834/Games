#ifndef _MATH_UTILITY_H_
#define _MATH_UTILITY_H_

#include <gslib.h>

class MathUtility
{
public:
	static const float PI; // �~����
	static float sin(float degree); // �T�C��
	static float cos(float degree); // �R�T�C��
	static float tan(float degree); // �^���W�F���g
	static float asin(float x); // �t�T�C���i�T�C���l����p�x�����߂�j
	static float acos(float x); // �t�R�T�C���i�R�T�C���l����p�x�����߂�j
	static float atan(float y, float x); // �t�^���W�F���g�i�^���W�F���g�l����p�x�����߂�j
	static float radian(float degree); // �p�x�����W�A���ɕϊ�
	static float degree(float radian); // ���W�A�����p�x�ɕϊ�
	static float angle(const GSvector2& v); //�x�N�g���̌����Ă���p�x�����߂�(�x�N�g������p�x���v�Z����)
	static float angle(const GSvector2& my, const GSvector2& target); //�^�[�Q�b�g�����ւ̊p�x�����߂�
	static float normalizeAngle(float angle); //�p�x�̐��K��(-360.0f�`360.0f)
	static float subtractAngle(float my, float target); //�p�x�������߂�
	static float subtractAngle(const GSvector2& my, float dir, const GSvector2& target); //�^�[�Q�b�g�����ւ̊p�x�������߂�
	static GSvector2 lerp(const GSvector2& start, const GSvector2& end, float t); //���`��Ԋ֐�
	static GSvector2 interpolatePower(const GSvector2& start, const GSvector2& end, float t, float power); //�ׂ���֐����g������Ԋ֐�
	static GSvector2 interpolateSinPower(const GSvector2& start, const GSvector2& end, float t, float power); //�ׂ���֐��ƎO�p�֐����g������Ԋ֐�
};

#endif