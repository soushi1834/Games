#include "CsvReader.h"
#include <fstream>
#include <sstream>
#include <iostream>


CsvReader::CsvReader()
{}

CsvReader::~CsvReader()
{}

//�f�[�^�̓ǂݍ���
bool CsvReader::load(const std::string& fileName)
{
	mData.clear();

	std::ifstream file(fileName);
	if (!file)
	{
		return false;
	}

	std::string line;
	//�s�����J��Ԃ�
	while (std::getline(file, line))
	{
		std::stringstream stream(line);	//�����ݒ�\������
		Row row;
		std::string val;
		//�񐔕��J��Ԃ�
		while (std::getline(stream, val, ','))
		{
			row.push_back(val);
		}
		mData.push_back(row);
	}
	return true;
}

//�f�[�^�̍s���擾
int CsvReader::getRows() const
{
	return mData.size();
}
//�f�[�^�̗���擾
int CsvReader::getColumns(int row) const
{
	if (0 == getRows())
	{
		return 0;
	}
	return mData[row].size();
}

//�f�[�^�𕶎���Ŏ擾
const std::string& CsvReader::getDataToString(int row, int col) const
{
	return mData[row][col];
}

//�f�[�^��int�Ŏ擾
int CsvReader::getDataToInteger(int row, int col) const
{
	return std::atoi(getDataToString(row, col).c_str());
}

//�f�[�^��float�Ŏ擾
float CsvReader::getDataToFloat(int row, int col) const
{
	return (float)getDataToDouble(row, col);
}

//�f�[�^��double�Ŏ擾
double CsvReader::getDataToDouble(int row, int col) const
{
	return std::atof(getDataToString(row, col).c_str());
}