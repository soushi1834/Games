#ifndef _DRAW_SPRITE_2D_H_
#define _DRAW_SPRITE_2D_H_

#include <gslib.h>

#ifdef __cplusplus
extern "C" {
#endif

void DrawSprite2D
	(
		GSuint				uTextureID,		//テクスチャＩＤ
		const GSrect*		pSrcRect,		//テクスチャ内の位置
		const GSvector2*	pCenter,		//スプライトの中心位置
		const GSvector2*	pScaling,		//スケール値
		GSfloat				fRotation,		//回転角度
		const GSvector2*	pTrancelation,	//平行移動量
		const GScolor*		pColor			//カラー
	);

#ifdef __cplusplus
}
#endif

#endif