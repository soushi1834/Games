#include "EnemyMove.h"
#include "EnemyBulletID.h"
#include "MathUtility.h"

EnemyMove::EnemyMove(IWorld& world, GSvector2 start) :
bullet(EnemyBulletGenerator(world)),
start_(start),
vector_(GSvector2(0, 0)),
count(0)
{}

EnemyMove::~EnemyMove()
{}

GSvector2 EnemyMove::move(GSvector2 position, int type)
{
	//if�����(�v����)
	switch (type)
	{
		//�o�đS���ʒe�����ċ���
	case 0:
		if (count <= 60)
			vector_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 60.0f, 0.5f) - position;
		else if (count >= 120)
			vector_ = MathUtility::interpolatePower(start_ + GSvector2(0, 192), start_ - GSvector2(0, 16), (count - 120) / 60.0f, 2.0f) - position;
		else
		{
			vector_ = GSvector2(0, 0);
			if (count == 90)
				bullet.fire(position, �Q�S����, 4);
		}
		break;

		//�o�Ď��@�_���S���ʒe�����ċ���
	case 14:
		if (count <= 60)
			vector_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 60.0f, 0.5f) - position;
		else if (count >= 120)
			vector_ = MathUtility::interpolatePower(start_ + GSvector2(0, 192), start_ - GSvector2(0, 16), (count - 120) / 60.0f, 2.0f) - position;
		else
		{
			vector_ = GSvector2(0, 0);
			if (count == 90)
				bullet.fire(position, ���@�_���Q�S����, 4);
		}
		break;

		//�o�Ă΂�܂��ċ���
	case 5:
		if (count <= 60)
			vector_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 60.0f, 0.5f) - position;
		else if (count >= 300)
			vector_ = MathUtility::interpolatePower(start_ + GSvector2(0, 192), start_ - GSvector2(0, 16), (count - 300) / 60.0f, 2.0f) - position;
		else
		{
			vector_ = GSvector2(0, 0);
			if (count >= 75 && count <= 285)
				bullet.fire(position, �΂�܂�, gsRandf(2, 6));
		}
		break;

		//�o�Ď��@�_���ܕ��ʌ����ċ���
	case 6:
		if (count <= 60)
			vector_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 60.0f, 0.5f) - position;
		else if (count >= 120)
			vector_ = MathUtility::interpolatePower(start_ + GSvector2(0, 192), start_ - GSvector2(0, 16), (count - 120) / 60.0f, 2.0f) - position;
		else
		{
			vector_ = GSvector2(0, 0);
			if (count == 90)
				bullet.fire(position, ���@�_���O���T����, 4);
		}
		break;

		//�o�Đ����ϑ��e�����ċ���
	case 9:
		if (count <= 60)
			vector_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 60.0f, 0.5f) - position;
		else if (count >= 120)
			vector_ = MathUtility::interpolatePower(start_ + GSvector2(0, 192), start_ - GSvector2(0, 16), (count - 120) / 60.0f, 2.0f) - position;
		else
		{
			vector_ = GSvector2(0, 0);
			if (count == 90)
			{
				bullet.fire(position, �����e, 8);
			}
		}
		break;

		//�o�ă����_����16�������ċ���
	case 10:
		if (count <= 60)
			vector_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 60.0f, 0.5f) - position;
		else if (count >= 90)
			vector_ = MathUtility::interpolatePower(start_ + GSvector2(0, 192), start_ - GSvector2(0, 16), (count - 90) / 60.0f, 2.0f) - position;
		else
		{
			vector_ = GSvector2(0, 0);
			if (count == 75)
			{
				for (int i = 0; i < 16; i++)
				{
					bullet.fire(position, �΂�܂�, 4);
				}
			}
		}
		break;

		//S������
	case 1:
		if (count <= 45)
			vector_ = GSvector2(0.0f, 3.0f);
		else if (count <= 135)
			vector_ = GSvector2(MathUtility::sin(count * 2 - 90) * 2, 3.0f);
		else
			vector_ = GSvector2(0.0f, 3.0f);
		break;

		//�tS������
	case 2:
		if (count <= 45)
			vector_ = GSvector2(0.0f, 3.0f);
		else if (count <= 135)
			vector_ = GSvector2(-MathUtility::sin(count * 2 - 90) * 2, 3.0f);
		else
			vector_ = GSvector2(0.0f, 3.0f);
		break;

		//�E�Ȃ藎��
	case 3:
		vector_ = GSvector2(count / 120.0f, 3.0f);
		break;

		//���Ȃ藎��
	case 4:
		vector_ = GSvector2(-count / 120.0f, 3.0f);
		break;

		//�傫�������񂵂Ȃ��玩�@�_���e
	case 7:
		vector_ = GSvector2(2.0f - pow(count, 2.0f) / 4000.0f, 1.0f);
		if (count % 50 == 0 && count > 0)
			bullet.fire(position, ���@�_���e, 4);
		break;

		//�傫���E���񂵂Ȃ��玩�@�_���e
	case 8:
		vector_ = GSvector2(-2.0f + pow(count, 2.0f) / 4000.0f, 1.0f);
		if (count % 50 == 0 && count > 0)
			bullet.fire(position, ���@�_���e, 4);
		break;

		//�o�ċ���
	case 11:
		if (count <= 60)
			vector_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 60.0f, 0.5f) - position;
		else if (count >= 120)
			vector_ = MathUtility::interpolatePower(start_ + GSvector2(0, 192), start_ - GSvector2(0, 16), (count - 120) / 60.0f, 2.0f) - position;
		else
			vector_ = GSvector2(0, 0);
		break;

		//�����牡�؂�
	case 12:
		vector_ = GSvector2(8, 2);
		break;

		//�E���炩�牡�؂�
	case 13:
		vector_ = GSvector2(-8, 2);
		break;

	default:
		break;
	}

	count++;
	return vector_;
}