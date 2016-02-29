#include "DrawSprite2D.h"

void DrawSprite2D
	(
		GSuint				uTextureID,		//テクスチャＩＤ
		const GSrect*		pSrcRect,		//テクスチャ内の位置
		const GSvector2*	pCenter,		//スプライトの中心位置
		const GSvector2*	pScaling,		//スケール値
		GSfloat				fRotation,		//回転角度
		const GSvector2*	pTrancelation,	//平行移動量
		const GScolor*		pColor			//カラー
	)
{
	GSrect		rTexCoord;
	GLfloat		fWidth;
	GLfloat		fHeight;
	GLsizei		sTexWidth;
	GLsizei		sTexHeight;
	GScolor		CurrentColor;
	//ビューポートの配列(C言語用宣言)
	int viewpoint[4];

	//各種レンダリングモードの退避
	glPushAttrib( GL_ENABLE_BIT );

	//ライティングを無効にする
	glDisable( GL_LIGHTING );

	//ｚバッファを無効にする
	glDisable( GL_DEPTH_TEST );

	//面カリングを無効化する
	glDisable( GL_CULL_FACE );

	//カレントカラーを取得する
	glGetFloatv( GL_CURRENT_COLOR, (GLfloat*)&CurrentColor );

	//テクスチャをバインドする
	gsBindTexture( uTextureID );

	//透視変換行列の退避
	glMatrixMode( GL_PROJECTION );
	glPushMatrix();

	//ビューポートの取得
	//int viewpoint[4]; //(C++用宣言)
	glGetIntegerv(GL_VIEWPORT, viewpoint);

	//正射影の変換行列を設定
	glLoadIdentity();
	gluOrtho2D(0.0f, (float)viewpoint[2], (float)viewpoint[3], 0.0f);

	//モデルビュー変換行列の退避
	glMatrixMode( GL_MODELVIEW );
	glPushMatrix();

	//変換行列の初期化
	glLoadIdentity();

	//平行移動量の設定を行う
	if( pTrancelation != NULL )
	{
		glTranslatef( pTrancelation->x, pTrancelation->y, 0.0f );
	}

	//回転角度の設定を行う
	glRotatef( fRotation, 0.0f, 0.0f, 1.0f );

	//拡大縮小を行う
	if ( pScaling != NULL )
	{
		glScalef( pScaling->x, pScaling->y, 0.0f );
	}

	//中心位置の補正を行う
	if ( pCenter != NULL )
	{
		glTranslatef( -pCenter->x, -pCenter->y, 0 );
	}

	//頂点カラーの設定
	if ( pColor != NULL )
	{
		glColor4fv( (GLfloat*)pColor );
	}

	//バインド中のテクスチャの幅と高さを取得
	glGetTexLevelParameteriv( GL_TEXTURE_2D, 0, GL_TEXTURE_WIDTH, &sTexWidth );
	glGetTexLevelParameteriv( GL_TEXTURE_2D, 0, GL_TEXTURE_HEIGHT, &sTexHeight );

	if( pSrcRect != NULL )
	{
		//テクスチャ座標の計算を行う
		rTexCoord.left		= pSrcRect->left	/ sTexWidth;
		rTexCoord.top		= pSrcRect->top		/ sTexHeight;
		rTexCoord.right		= pSrcRect->right	/ sTexWidth;
		rTexCoord.bottom	= pSrcRect->bottom	/ sTexHeight;

		//四角形の幅と高さを求める
		fWidth = ABS( pSrcRect->right - pSrcRect->left );
		fHeight = ABS( pSrcRect->bottom - pSrcRect->top );
	}
	else
	{
		//テクスチャ内の位置が指定されていない場合はテクスチャを全体表示
		rTexCoord.left		= 0.0f;
		rTexCoord.top		= 0.0f;
		rTexCoord.right		= 1.0f;
		rTexCoord.bottom	= 1.0f;

		fWidth = (GLfloat)sTexWidth;
		fHeight = (GLfloat)sTexHeight;
	}

	//四角形の描画
	glBegin( GL_QUADS );
	glTexCoord2f( rTexCoord.left, rTexCoord.top );
	glVertex2f( 0, 0 );
	glTexCoord2f( rTexCoord.left, rTexCoord.bottom );
	glVertex2f( 0, fHeight );
	glTexCoord2f( rTexCoord.right, rTexCoord.bottom );
	glVertex2f( fWidth, fHeight );
	glTexCoord2f( rTexCoord.right, rTexCoord.top );
	glVertex2f( fWidth, 0 );
	glEnd();

	//モデルビュー変換行列を復帰
	glPopMatrix();

	//透視変換行列を復帰
	glMatrixMode( GL_PROJECTION );
	glPopMatrix();

	//モデルビュー変換行列に設定
	glMatrixMode( GL_MODELVIEW );

	//カレントカラーを復帰する
	glColor4fv( (GLfloat*)&CurrentColor );

	//レンダリングモードの復帰
	glPopAttrib();
}