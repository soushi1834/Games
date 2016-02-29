#include "CsvReader.h"
#include <fstream>
#include <sstream>
#include <iostream>


CsvReader::CsvReader()
{}

CsvReader::~CsvReader()
{}

//データの読み込み
bool CsvReader::load(const std::string& fileName)
{
	mData.clear();

	std::ifstream file(fileName);
	if (!file)
	{
		return false;
	}

	std::string line;
	//行数分繰り返し
	while (std::getline(file, line))
	{
		std::stringstream stream(line);	//書式設定可能文字列
		Row row;
		std::string val;
		//列数分繰り返し
		while (std::getline(stream, val, ','))
		{
			row.push_back(val);
		}
		mData.push_back(row);
	}
	return true;
}

//データの行を取得
int CsvReader::getRows() const
{
	return mData.size();
}
//データの列を取得
int CsvReader::getColumns(int row) const
{
	if (0 == getRows())
	{
		return 0;
	}
	return mData[row].size();
}

//データを文字列で取得
const std::string& CsvReader::getDataToString(int row, int col) const
{
	return mData[row][col];
}

//データをintで取得
int CsvReader::getDataToInteger(int row, int col) const
{
	return std::atoi(getDataToString(row, col).c_str());
}

//データをfloatで取得
float CsvReader::getDataToFloat(int row, int col) const
{
	return (float)getDataToDouble(row, col);
}

//データをdoubleで取得
double CsvReader::getDataToDouble(int row, int col) const
{
	return std::atof(getDataToString(row, col).c_str());
}