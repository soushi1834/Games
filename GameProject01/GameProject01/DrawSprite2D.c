#include "DrawSprite2D.h"

void DrawSprite2D
	(
		GSuint				uTextureID,		//�e�N�X�`���h�c
		const GSrect*		pSrcRect,		//�e�N�X�`�����̈ʒu
		const GSvector2*	pCenter,		//�X�v���C�g�̒��S�ʒu
		const GSvector2*	pScaling,		//�X�P�[���l
		GSfloat				fRotation,		//��]�p�x
		const GSvector2*	pTrancelation,	//���s�ړ���
		const GScolor*		pColor			//�J���[
	)
{
	GSrect		rTexCoord;
	GLfloat		fWidth;
	GLfloat		fHeight;
	GLsizei		sTexWidth;
	GLsizei		sTexHeight;
	GScolor		CurrentColor;
	//�r���[�|�[�g�̔z��(C����p�錾)
	int viewpoint[4];

	//�e�탌���_�����O���[�h�̑ޔ�
	glPushAttrib( GL_ENABLE_BIT );

	//���C�e�B���O�𖳌��ɂ���
	glDisable( GL_LIGHTING );

	//���o�b�t�@�𖳌��ɂ���
	glDisable( GL_DEPTH_TEST );

	//�ʃJ�����O�𖳌�������
	glDisable( GL_CULL_FACE );

	//�J�����g�J���[���擾����
	glGetFloatv( GL_CURRENT_COLOR, (GLfloat*)&CurrentColor );

	//�e�N�X�`�����o�C���h����
	gsBindTexture( uTextureID );

	//�����ϊ��s��̑ޔ�
	glMatrixMode( GL_PROJECTION );
	glPushMatrix();

	//�r���[�|�[�g�̎擾
	//int viewpoint[4]; //(C++�p�錾)
	glGetIntegerv(GL_VIEWPORT, viewpoint);

	//���ˉe�̕ϊ��s���ݒ�
	glLoadIdentity();
	gluOrtho2D(0.0f, (float)viewpoint[2], (float)viewpoint[3], 0.0f);

	//���f���r���[�ϊ��s��̑ޔ�
	glMatrixMode( GL_MODELVIEW );
	glPushMatrix();

	//�ϊ��s��̏�����
	glLoadIdentity();

	//���s�ړ��ʂ̐ݒ���s��
	if( pTrancelation != NULL )
	{
		glTranslatef( pTrancelation->x, pTrancelation->y, 0.0f );
	}

	//��]�p�x�̐ݒ���s��
	glRotatef( fRotation, 0.0f, 0.0f, 1.0f );

	//�g��k�����s��
	if ( pScaling != NULL )
	{
		glScalef( pScaling->x, pScaling->y, 0.0f );
	}

	//���S�ʒu�̕␳���s��
	if ( pCenter != NULL )
	{
		glTranslatef( -pCenter->x, -pCenter->y, 0 );
	}

	//���_�J���[�̐ݒ�
	if ( pColor != NULL )
	{
		glColor4fv( (GLfloat*)pColor );
	}

	//�o�C���h���̃e�N�X�`���̕��ƍ������擾
	glGetTexLevelParameteriv( GL_TEXTURE_2D, 0, GL_TEXTURE_WIDTH, &sTexWidth );
	glGetTexLevelParameteriv( GL_TEXTURE_2D, 0, GL_TEXTURE_HEIGHT, &sTexHeight );

	if( pSrcRect != NULL )
	{
		//�e�N�X�`�����W�̌v�Z���s��
		rTexCoord.left		= pSrcRect->left	/ sTexWidth;
		rTexCoord.top		= pSrcRect->top		/ sTexHeight;
		rTexCoord.right		= pSrcRect->right	/ sTexWidth;
		rTexCoord.bottom	= pSrcRect->bottom	/ sTexHeight;

		//�l�p�`�̕��ƍ��������߂�
		fWidth = ABS( pSrcRect->right - pSrcRect->left );
		fHeight = ABS( pSrcRect->bottom - pSrcRect->top );
	}
	else
	{
		//�e�N�X�`�����̈ʒu���w�肳��Ă��Ȃ��ꍇ�̓e�N�X�`����S�̕\��
		rTexCoord.left		= 0.0f;
		rTexCoord.top		= 0.0f;
		rTexCoord.right		= 1.0f;
		rTexCoord.bottom	= 1.0f;

		fWidth = (GLfloat)sTexWidth;
		fHeight = (GLfloat)sTexHeight;
	}

	//�l�p�`�̕`��
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

	//���f���r���[�ϊ��s��𕜋A
	glPopMatrix();

	//�����ϊ��s��𕜋A
	glMatrixMode( GL_PROJECTION );
	glPopMatrix();

	//���f���r���[�ϊ��s��ɐݒ�
	glMatrixMode( GL_MODELVIEW );

	//�J�����g�J���[�𕜋A����
	glColor4fv( (GLfloat*)&CurrentColor );

	//�����_�����O���[�h�̕��A
	glPopAttrib();
}