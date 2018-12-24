using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TemplateMod.Utils
{
	public struct MyMatrix
	{
		public int Rows { get; }
		public int Cols { get; }

		private double[,] _content;

		public MyMatrix(int row, int col)
		{
			Rows = row;
			Cols = col;
			_content = new double[Rows, Cols];
			ClearOut();
		}

		public MyMatrix(int dim)
		{
			Rows = Cols = dim;
			_content = new double[dim, dim];
			ClearOut();
		}

		public MyMatrix(double[,] datas)
		{
			Rows = datas.GetLength(0);
			Cols = datas.GetLength(1);
			_content = new double[Rows, Cols];
			SetValue(datas);
		}

		public void SetValue(double[,] datas)
		{
			if (datas.GetLength(1) != this.Cols || datas.GetLength(0) != this.Rows)
				throw new ArgumentException("矩阵维度不匹配！");

			for(int r = 0; r < Rows; r++)
			{
				for(int c = 0; c < Cols; c++)
				{
					_content[r, c] = datas[r, c];
				}
			}
			
		}

		private void ClearOut()
		{
			for (int r = 0; r < Rows; r++)
			{
				for (int c = 0; c < Cols; c++)
				{
					_content[r, c] = 0.0;
				}
			}
		}

		public static MyMatrix operator* (MyMatrix m1, MyMatrix m2)
		{
			if (m1.Cols != m2.Rows)
				throw new ArgumentException("乘法：矩阵两边维度不匹配！");
			MyMatrix ans = new MyMatrix(m1.Rows, m2.Cols);
			for (int i = 0; i < ans.Rows; i++)
				for (int j = 0; j < ans.Cols; j++)
					for (int k = 0; k < m2.Rows; k++)
						ans[i, j] += m1[i, k] * m2[k, j];
			return ans;
		}
		public double this[int row, int col]
		{
			get { return _content[row, col]; }
			set { _content[row, col] = value; }
		}


		public static MyMatrix FromVector2(Vector2 v)
		{
			MyMatrix ret = new MyMatrix(3, 1);
			double[,] arr = new double[3, 1]
			{
				{ v.X },
				{ v.Y },
				{ 1.0 }
			};
			ret.SetValue(arr);
			return ret;
		}

		public static MyMatrix FromVector24(Vector2 v)
		{
			MyMatrix ret = new MyMatrix(4, 1);
			double[,] arr = new double[4, 1]
			{
				{ v.X },
				{ v.Y },
				{ 1.0 },
				{ 1.0 }
			};
			ret.SetValue(arr);
			return ret;
		}

		public static MyMatrix FromVector34(Vector3 v)
		{
			MyMatrix ret = new MyMatrix(4, 1);
			double[,] arr = new double[4, 1]
			{
				{ v.X },
				{ v.Y },
				{ v.Z },
				{ 1.0 }
			};
			ret.SetValue(arr);
			return ret;
		}
		public static MyMatrix FromVector44(Vector4 v)
		{
			MyMatrix ret = new MyMatrix(5, 1);
			double[,] arr = new double[5, 1]
			{
				{ v.X },
				{ v.Y },
				{ v.Z },
				{ v.W },
				{ 1.0 }
			};
			ret.SetValue(arr);
			return ret;
		}

		public Vector2 ExtractVector2()
		{
			return new Vector2((float)_content[0, 0], (float)_content[1, 0]);
		}

		public static MyMatrix RotationMatrixTY(double radians)
		{
			//return new MyMatrix(new double[3, 3]
			//   {
			//		{Math.Cos(radians), -Math.Sin(radians), 0.0},
			//		{Math.Sin(radians), Math.Cos(radians),  0.0},
			//		{0.0,           0.0,            1.0}
			//   });
			return new MyMatrix(new double[4, 4]
			   {
					{Math.Cos(radians),  0.0, -Math.Sin(radians), 0.0},
				   	{0.0, 1.0, 0.0 , 0.0},
					{Math.Sin(radians), 0.0, Math.Cos(radians), 0.0},
					{0.0, 0.0, 0.0, 1.0}
			   });
		}
		public static MyMatrix RotationMatrixT4Y(double radians)
		{
			//return new MyMatrix(new double[3, 3]
			//   {
			//		{Math.Cos(radians), -Math.Sin(radians), 0.0},
			//		{Math.Sin(radians), Math.Cos(radians),  0.0},
			//		{0.0,           0.0,            1.0}
			//   });
			return new MyMatrix(new double[5, 5]
			   {
					{Math.Cos(radians),  0.0, -Math.Sin(radians), 0.0, 0.0},
				   	{0.0, 1.0, 0.0 , 0.0, 0.0},
					{Math.Sin(radians), 0.0, Math.Cos(radians), 0.0, 0.0},
					{0.0, 0.0, 0.0, 1.0, 0.0},
					{0.0, 0.0, 0.0, 0.0, 1.0},
			   });
		}

		public static MyMatrix RotationMatrixTX(double radians)
		{
			//return new MyMatrix(new double[3, 3]
			//   {
			//		{Math.Cos(radians), -Math.Sin(radians), 0.0},
			//		{Math.Sin(radians), Math.Cos(radians),  0.0},
			//		{0.0,           0.0,            1.0}
			//   });
			return new MyMatrix(new double[4, 4]
			   {
					{1.0,  0.0, 0.0, 0.0},
				   	{0.0, Math.Cos(radians), Math.Sin(radians) , 0.0},
					{0.0, -Math.Sin(radians), Math.Cos(radians), 0.0},
					{0.0, 0.0, 0.0, 1.0}
			   });
		}

		public static MyMatrix RotationMatrixTZ(double radians)
		{
			//return new MyMatrix(new double[3, 3]
			//   {
			//		{Math.Cos(radians), -Math.Sin(radians), 0.0},
			//		{Math.Sin(radians), Math.Cos(radians),  0.0},
			//		{0.0,           0.0,            1.0}
			//   });
			return new MyMatrix(new double[4, 4]
			   {
					{Math.Cos(radians), -Math.Sin(radians), 0.0, 0.0},
					{Math.Sin(radians), Math.Cos(radians),  0.0, 0.0},
					{0.0,           0.0,            1.0, 0.0},
					{0.0, 0.0, 0.0, 1.0}
			   });
		}

		public static MyMatrix ScaleMatrixT(double x = 1.0, double y = 1.0, double z = 1.0)
		{
			//return new MyMatrix(new double[3, 3]
			//   {
			//		{Math.Cos(radians), -Math.Sin(radians), 0.0},
			//		{Math.Sin(radians), Math.Cos(radians),  0.0},
			//		{0.0,           0.0,            1.0}
			//   });
			return new MyMatrix(new double[4, 4]
			{
				{x, 0.0, 0.0, 0.0},
				{0.0, y, 0.0, 0.0},
				{0.0, 0.0, z, 0.0},
				{0.0, 0.0, 0.0, 1.0}
			});
		}


		public static MyMatrix RotationMatrix(double radians)
		{
			return new MyMatrix(new double[3, 3]
			   {
					{Math.Cos(radians), -Math.Sin(radians), 0.0},
					{Math.Sin(radians), Math.Cos(radians),  0.0},
					{0.0,           0.0,            1.0}
			   });

		}
		public static MyMatrix TranslationMatrix(double x, double y)
		{
			return new MyMatrix(new double[3, 3]
				{
					{1.0, 0.0, x},
					{0.0, 1.0, y},
					{0.0, 0.0, 1.0 }
				});
		}

		public static MyMatrix ScaleMatrix(double s)
		{
			return new MyMatrix(new double[3, 3]
				{
					{s, 0.0, 0.0},
					{0.0, s, 0.0},
					{0.0, 0.0, 1.0 }
				});
		}
	}
}
