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
	//if•¶‘å—Ê(—v‰ü—Ç)
	switch (type)
	{
		//o‚Ä‘S•ûˆÊ’eŒ‚‚Á‚Ä‹‚é
	case 0:
		if (count <= 60)
			vector_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 60.0f, 0.5f) - position;
		else if (count >= 120)
			vector_ = MathUtility::interpolatePower(start_ + GSvector2(0, 192), start_ - GSvector2(0, 16), (count - 120) / 60.0f, 2.0f) - position;
		else
		{
			vector_ = GSvector2(0, 0);
			if (count == 90)
				bullet.fire(position, ‚Q‚S•ûˆÊ, 4);
		}
		break;

		//o‚Ä©‹@‘_‚¢‘S•ûˆÊ’eŒ‚‚Á‚Ä‹‚é
	case 14:
		if (count <= 60)
			vector_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 60.0f, 0.5f) - position;
		else if (count >= 120)
			vector_ = MathUtility::interpolatePower(start_ + GSvector2(0, 192), start_ - GSvector2(0, 16), (count - 120) / 60.0f, 2.0f) - position;
		else
		{
			vector_ = GSvector2(0, 0);
			if (count == 90)
				bullet.fire(position, ©‹@‘_‚¢‚Q‚S•ûˆÊ, 4);
		}
		break;

		//o‚Ä‚Î‚ç‚Ü‚¢‚Ä‹‚é
	case 5:
		if (count <= 60)
			vector_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 60.0f, 0.5f) - position;
		else if (count >= 300)
			vector_ = MathUtility::interpolatePower(start_ + GSvector2(0, 192), start_ - GSvector2(0, 16), (count - 300) / 60.0f, 2.0f) - position;
		else
		{
			vector_ = GSvector2(0, 0);
			if (count >= 75 && count <= 285)
				bullet.fire(position, ‚Î‚ç‚Ü‚«, gsRandf(2, 6));
		}
		break;

		//o‚Ä©‹@‘_‚¢ŒÜ•ûˆÊŒ‚‚Á‚Ä‹‚é
	case 6:
		if (count <= 60)
			vector_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 60.0f, 0.5f) - position;
		else if (count >= 120)
			vector_ = MathUtility::interpolatePower(start_ + GSvector2(0, 192), start_ - GSvector2(0, 16), (count - 120) / 60.0f, 2.0f) - position;
		else
		{
			vector_ = GSvector2(0, 0);
			if (count == 90)
				bullet.fire(position, ©‹@‘_‚¢‘O•û‚T•ûˆÊ, 4);
		}
		break;

		//o‚Ä‚’¼•Ï‘¬’eŒ‚‚Á‚Ä‹‚é
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
				bullet.fire(position, ‚’¼’e, 8);
			}
		}
		break;

		//o‚Äƒ‰ƒ“ƒ_ƒ€‚É16”­Œ‚‚Á‚Ä‹‚é
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
					bullet.fire(position, ‚Î‚ç‚Ü‚«, 4);
				}
			}
		}
		break;

		//Sš—‰º
	case 1:
		if (count <= 45)
			vector_ = GSvector2(0.0f, 3.0f);
		else if (count <= 135)
			vector_ = GSvector2(MathUtility::sin(count * 2 - 90) * 2, 3.0f);
		else
			vector_ = GSvector2(0.0f, 3.0f);
		break;

		//‹tSš—‰º
	case 2:
		if (count <= 45)
			vector_ = GSvector2(0.0f, 3.0f);
		else if (count <= 135)
			vector_ = GSvector2(-MathUtility::sin(count * 2 - 90) * 2, 3.0f);
		else
			vector_ = GSvector2(0.0f, 3.0f);
		break;

		//‰E‹È‚è—‰º
	case 3:
		vector_ = GSvector2(count / 120.0f, 3.0f);
		break;

		//¶‹È‚è—‰º
	case 4:
		vector_ = GSvector2(-count / 120.0f, 3.0f);
		break;

		//‘å‚«‚­¶ù‰ñ‚µ‚È‚ª‚ç©‹@‘_‚¢’e
	case 7:
		vector_ = GSvector2(2.0f - pow(count, 2.0f) / 4000.0f, 1.0f);
		if (count % 50 == 0 && count > 0)
			bullet.fire(position, ©‹@‘_‚¢’e, 4);
		break;

		//‘å‚«‚­‰Eù‰ñ‚µ‚È‚ª‚ç©‹@‘_‚¢’e
	case 8:
		vector_ = GSvector2(-2.0f + pow(count, 2.0f) / 4000.0f, 1.0f);
		if (count % 50 == 0 && count > 0)
			bullet.fire(position, ©‹@‘_‚¢’e, 4);
		break;

		//o‚Ä‹‚é
	case 11:
		if (count <= 60)
			vector_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 60.0f, 0.5f) - position;
		else if (count >= 120)
			vector_ = MathUtility::interpolatePower(start_ + GSvector2(0, 192), start_ - GSvector2(0, 16), (count - 120) / 60.0f, 2.0f) - position;
		else
			vector_ = GSvector2(0, 0);
		break;

		//¶‚©‚ç‰¡Ø‚é
	case 12:
		vector_ = GSvector2(8, 2);
		break;

		//‰E‚©‚ç‚©‚ç‰¡Ø‚é
	case 13:
		vector_ = GSvector2(-8, 2);
		break;

	default:
		break;
	}

	count++;
	return vector_;
}