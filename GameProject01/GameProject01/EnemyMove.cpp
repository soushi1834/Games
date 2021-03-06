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
	//if文大量(要改良)
	switch (type)
	{
		//出て全方位弾撃って去る
	case 0:
		if (count <= 60)
			vector_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 60.0f, 0.5f) - position;
		else if (count >= 120)
			vector_ = MathUtility::interpolatePower(start_ + GSvector2(0, 192), start_ - GSvector2(0, 16), (count - 120) / 60.0f, 2.0f) - position;
		else
		{
			vector_ = GSvector2(0, 0);
			if (count == 90)
				bullet.fire(position, ２４方位, 4);
		}
		break;

		//出て自機狙い全方位弾撃って去る
	case 14:
		if (count <= 60)
			vector_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 60.0f, 0.5f) - position;
		else if (count >= 120)
			vector_ = MathUtility::interpolatePower(start_ + GSvector2(0, 192), start_ - GSvector2(0, 16), (count - 120) / 60.0f, 2.0f) - position;
		else
		{
			vector_ = GSvector2(0, 0);
			if (count == 90)
				bullet.fire(position, 自機狙い２４方位, 4);
		}
		break;

		//出てばらまいて去る
	case 5:
		if (count <= 60)
			vector_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 60.0f, 0.5f) - position;
		else if (count >= 300)
			vector_ = MathUtility::interpolatePower(start_ + GSvector2(0, 192), start_ - GSvector2(0, 16), (count - 300) / 60.0f, 2.0f) - position;
		else
		{
			vector_ = GSvector2(0, 0);
			if (count >= 75 && count <= 285)
				bullet.fire(position, ばらまき, gsRandf(2, 6));
		}
		break;

		//出て自機狙い五方位撃って去る
	case 6:
		if (count <= 60)
			vector_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 60.0f, 0.5f) - position;
		else if (count >= 120)
			vector_ = MathUtility::interpolatePower(start_ + GSvector2(0, 192), start_ - GSvector2(0, 16), (count - 120) / 60.0f, 2.0f) - position;
		else
		{
			vector_ = GSvector2(0, 0);
			if (count == 90)
				bullet.fire(position, 自機狙い前方５方位, 4);
		}
		break;

		//出て垂直変速弾撃って去る
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
				bullet.fire(position, 垂直弾, 8);
			}
		}
		break;

		//出てランダムに16発撃って去る
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
					bullet.fire(position, ばらまき, 4);
				}
			}
		}
		break;

		//S字落下
	case 1:
		if (count <= 45)
			vector_ = GSvector2(0.0f, 3.0f);
		else if (count <= 135)
			vector_ = GSvector2(MathUtility::sin(count * 2 - 90) * 2, 3.0f);
		else
			vector_ = GSvector2(0.0f, 3.0f);
		break;

		//逆S字落下
	case 2:
		if (count <= 45)
			vector_ = GSvector2(0.0f, 3.0f);
		else if (count <= 135)
			vector_ = GSvector2(-MathUtility::sin(count * 2 - 90) * 2, 3.0f);
		else
			vector_ = GSvector2(0.0f, 3.0f);
		break;

		//右曲り落下
	case 3:
		vector_ = GSvector2(count / 120.0f, 3.0f);
		break;

		//左曲り落下
	case 4:
		vector_ = GSvector2(-count / 120.0f, 3.0f);
		break;

		//大きく左旋回しながら自機狙い弾
	case 7:
		vector_ = GSvector2(2.0f - pow(count, 2.0f) / 4000.0f, 1.0f);
		if (count % 50 == 0 && count > 0)
			bullet.fire(position, 自機狙い弾, 4);
		break;

		//大きく右旋回しながら自機狙い弾
	case 8:
		vector_ = GSvector2(-2.0f + pow(count, 2.0f) / 4000.0f, 1.0f);
		if (count % 50 == 0 && count > 0)
			bullet.fire(position, 自機狙い弾, 4);
		break;

		//出て去る
	case 11:
		if (count <= 60)
			vector_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 60.0f, 0.5f) - position;
		else if (count >= 120)
			vector_ = MathUtility::interpolatePower(start_ + GSvector2(0, 192), start_ - GSvector2(0, 16), (count - 120) / 60.0f, 2.0f) - position;
		else
			vector_ = GSvector2(0, 0);
		break;

		//左から横切る
	case 12:
		vector_ = GSvector2(8, 2);
		break;

		//右からから横切る
	case 13:
		vector_ = GSvector2(-8, 2);
		break;

	default:
		break;
	}

	count++;
	return vector_;
}