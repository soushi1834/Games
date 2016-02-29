#ifndef _DRAW_SPRITE_2D_H_
#define _DRAW_SPRITE_2D_H_

#include <gslib.h>

#ifdef __cplusplus
extern "C" {
#endif

void DrawSprite2D
	(
		GSuint				uTextureID,		//�e�N�X�`���h�c
		const GSrect*		pSrcRect,		//�e�N�X�`�����̈ʒu
		const GSvector2*	pCenter,		//�X�v���C�g�̒��S�ʒu
		const GSvector2*	pScaling,		//�X�P�[���l
		GSfloat				fRotation,		//��]�p�x
		const GSvector2*	pTrancelation,	//���s�ړ���
		const GScolor*		pColor			//�J���[
	);

#ifdef __cplusplus
}
#endif

#endif