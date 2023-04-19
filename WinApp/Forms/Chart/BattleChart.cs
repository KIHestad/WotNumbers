using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WinApp.Code;
using WinApp.Code.FormView;
using WinApp.Code.Rating;
using static WinApp.Code.FormView.BattleChartHelper;

namespace WinApp.Forms
{
	public partial class BattleChart : FormCloseOnEsc
	{
		#region Init

		int selectedChartFavouriteId = -1;
		string selectedChartFavouriteName = "";
		int initTankId = 0;
		int decimals = 3;
		int numPoints = 5000; // Max num of points in one chart serie, except for battle values (ChartValues.totals = false)

		// List of all available chart types 
		private List<BattleChartHelper.ChartType> chartTypeList = BattleChartHelper.GetChartTypeList(); 
		
		public BattleChart(int tankId = 0)
		{
			InitializeComponent();
			initTankId = tankId;
			// Add close form on pressing ESC
			this.KeyPreview = true;
			this.KeyDown += (sender, e) =>
			{
				if (e.KeyCode == Keys.Escape) this.Close();
			};
		}

		// To be able to minimize from task bar
		const int WS_MINIMIZEBOX = 0x20000;
		const int CS_DBLCLKS = 0x8;
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.Style |= WS_MINIMIZEBOX;
				cp.ClassStyle |= CS_DBLCLKS;
				return cp;
			}
		}
		
		private void SetMenuItems()
		{
			// Battle Modes, set menu
			SetBattleModeMenu();
			// Battle Time Filter, set menu
			SetBattleTimeFilterMenu();

			// Set x-axis
			mXaxisDate.Checked = (BattleChartHelper.Settings.Xaxis == mXaxisDate.Text);
			mXaxisBattle.Checked = (BattleChartHelper.Settings.Xaxis == mXaxisBattle.Text);

			// Set options
			mBullet.Checked = BattleChartHelper.Settings.Bullet;
			mSpline.Checked = BattleChartHelper.Settings.Spline;
		}

		private async void BattleChart_Load(object sender, EventArgs e)
		{
			// Toolstrip set width
			mMain.Width = this.Width - 2;

			// Set menu
			SetMenuItems();

			// Chart layout
			Font letterType = new Font("MS Sans Serif", 10, GraphicsUnit.Pixel);
			Color defaultColor = ColorTheme.ControlFont;
			ChartArea area = ChartingMain.ChartAreas[0];
			foreach (var axis in area.Axes)
			{
				axis.TitleForeColor = defaultColor;
				axis.LineColor = defaultColor;
				axis.InterlacedColor = defaultColor;
				axis.LabelStyle.Font = letterType;
				axis.LabelAutoFitMinFontSize = (int)letterType.Size;
				axis.LabelAutoFitMaxFontSize = (int)letterType.Size;
			}
			area.AxisX.LabelStyle.Angle = 20;

			ChartingMain.ChartAreas[0].AxisY2.IsStartedFromZero = ChartingMain.ChartAreas[0].AxisY.IsStartedFromZero;
			//ChartingMain.ChartAreas[0].AxisY2.LineDashStyle = ChartDashStyle.Dash;
			//ChartingMain.ChartAreas[0].AxisY2.LineColor = Color.Black;
			//ChartingMain.ChartAreas[0].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
			ChartingMain.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;

			ChartingMain.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
			ChartingMain.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

			ChartingMain.MouseWheel += BattleChart_MouseWheel;
			ChartingMain.MouseMove += BattleChart_MouseMove;

			// ChartingMain.ChartAreas[0].AxisX2.MouseWheel += 
			// Get favourites
			await SetFavouritMenu();
		}


		private void BattleChart_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				Axis ax = ChartingMain.ChartAreas[0].AxisX;
				Axis ay = ChartingMain.ChartAreas[0].AxisY;

				double xPosition = ax.ValueToPosition(ax.PixelPositionToValue(e.Location.X));
				double yPosition = ay.ValueToPosition(ay.PixelPositionToValue(e.Location.Y));

				lblFooter.Text = "( " + Convert.ToString(xPosition) + " , " + Convert.ToString(yPosition) + " ) - ScaleView Position (" + Convert.ToString(ax.ScaleView.Position) + " , " + Convert.ToString(ay.ScaleView.Position) + " )";
			}
			catch (Exception ex)
			{
			}
		}

		private void BattleChart_MouseWheel(object sender, MouseEventArgs e)
		{
			Axis ax = ChartingMain.ChartAreas[0].AxisX;
			Axis ay = ChartingMain.ChartAreas[0].AxisY;

			double pixelX = ax.ValueToPixelPosition(ax.ScaleView.ViewMinimum);
			double pixelY = ay.ValueToPixelPosition(ay.ScaleView.ViewMinimum);
			bool zoomIn = (e.Delta > 0);

			if (e.X < pixelX) // Mouse in the left area of the chart zoom horitontal
			{
				ZoomAxis(ay, zoomIn);
			}
			else if (e.Y > pixelY) // Mouse in the lower area of the chart zoom horitontal
			{
				ZoomAxis(ax, zoomIn);
			}
			else
			{
				// General zoom
				ZoomAxis(ax, zoomIn, e.Location.X);
				ZoomAxis(ay, zoomIn, e.Location.Y);
			}
		}

		private void ZoomAxis(Axis axis, bool zoomIn)
		{
			ZoomAxis(axis, zoomIn, false, 0.0);
		}

		private void ZoomAxis(Axis axis, bool zoomIn, double chartCoord)
		{
			ZoomAxis(axis, zoomIn, true, chartCoord);
		}

		private void ZoomAxis(Axis axis, bool zoomIn, bool handlePos, double chartCoord)
		{
			const double k_ZoomInScale = 1.5;
			double value = axis.PixelPositionToValue(chartCoord);

			if (zoomIn)
			{
				axis.ScaleView.Size = double.IsNaN(axis.ScaleView.Size) ? ((axis.Maximum - axis.Minimum) / k_ZoomInScale) : (axis.ScaleView.Size / k_ZoomInScale);
				if (handlePos)
				{
					HandleZoomPosition(axis, value);
				}
			}
			else
			{   
				axis.ScaleView.Size = double.IsNaN(axis.ScaleView.Size) ? axis.Maximum - axis.Minimum : (axis.ScaleView.Size * k_ZoomInScale);
				if (axis.ScaleView.Size > axis.Maximum - axis.Minimum)
				{
					axis.ScaleView.Size = axis.Maximum - axis.Minimum;
					axis.ScaleView.Position = axis.Minimum;
				}
				else
				{
					if (handlePos)
					{
						HandleZoomPosition(axis, value);
					}
				}
			}
		}

		private void HandleZoomPosition(Axis axis, double value)
		{
			double delta = axis.ScaleView.Size / 2;
			double position = value - delta;

			axis.ScaleView.Position = position;
			if (axis.ScaleView.Position + 2 * delta > axis.Maximum)
			{
				axis.ScaleView.Position = axis.Maximum - 2 * delta;
			}
			else if (axis.ScaleView.Position < axis.Minimum)
			{
				axis.ScaleView.Position = axis.Minimum;
			}
		}

		private async void BattleChart_Shown(object sender, EventArgs e)
		{
			if (initTankId != 0)
			{
				// Add chart item for selected tank (right click on tank from main form grid)
				BattleChartHelper.CurrentChartView = new List<BattleChartHelper.BattleChartItem>();
				OpenFormSelectChartParameters(initTankId);
			}
			else
			{
				// Get latest used favourite
				if (!String.IsNullOrEmpty(Config.Settings.currentChartFavourite))
				{
					selectedChartFavouriteId = -1;
					if (Int32.TryParse(Config.Settings.currentChartFavourite, out selectedChartFavouriteId))
					{
						if (selectedChartFavouriteId > 0)
							await GetFavouriteChart(selectedChartFavouriteId);
					}
				}
				// Draw current chart view
				await DrawCurrentChartView();
			}
		}

		#endregion

		#region Create Chart

		private string GetBattleTimeFilter(string where)
		{
			string newWhere = "";
			if (BattleChartHelper.Settings.BattleTime == "ALL")
				newWhere = where;
			else
			{
				DateTime afterDate = DateTime.Now;
				switch (BattleChartHelper.Settings.BattleTime)
				{
					case "Y2": afterDate = DateTime.Now.AddYears(-2); break;
					case "Y1": afterDate = DateTime.Now.AddYears(-1); break;
					case "M6": afterDate = DateTime.Now.AddMonths(-6); break;
					case "M3": afterDate = DateTime.Now.AddMonths(-3); break;
					case "M1": afterDate = DateTime.Now.AddMonths(-1); break;
					case "W2": afterDate = DateTime.Now.AddDays(-14); break;
					case "W1": afterDate = DateTime.Now.AddDays(-7); break;
					case "D3": afterDate = DateTime.Now.AddDays(-3); break;
					case "D2": afterDate = DateTime.Now.AddDays(-2); break;
					case "D1": afterDate = DateTime.Now.AddDays(-1); break;
				}
				newWhere = " battleTime >= '" + afterDate.ToString("yyyy-MM-dd") + "' ";
				if (where == "")
					newWhere = " where " + newWhere;
				else
					newWhere = where + " and " + newWhere;
			}

			return newWhere;
		}

		private double RoundOff(double min)
		{
			if (min <= 100)
				return Convert.ToDouble(Convert.ToInt32(min * 10)) / 10;
			else if (min <= 9999)
				return Convert.ToDouble(Math.Truncate(min));
			else
			{
				return min.RoundDown(1);
			}				
		}

		private async Task DrawCurrentChartView()
		{
			// Default = auto x-axis
			ChartingMain.ChartAreas[0].AxisX.Minimum = Double.NaN;
			// Check if 2 Yaxis
			bool yAxix1 = false;
			bool yAxix2 = false;
			foreach (BattleChartHelper.BattleChartItem item in BattleChartHelper.CurrentChartView)
			{
				if (item.Use2ndYaxis == false)
					yAxix1 = true;
				if (item.Use2ndYaxis == true)
					yAxix2 = true;
			}
			// Clear chart and prepare for yaxis
			ClearChartArea(yAxix1 && yAxix2);
			ChartingMain.ChartAreas[0].AxisX.ScaleView.Position = 0;
			ChartingMain.ChartAreas[0].AxisX.ScaleView.Position = 0;
			ChartingMain.ChartAreas[0].AxisX.ScaleView.Size = double.NaN;
			ChartingMain.ChartAreas[0].AxisY.ScaleView.Size = double.NaN;

			// Add chart values as series
			foreach (BattleChartHelper.BattleChartItem item in BattleChartHelper.CurrentChartView)
			{
				if (yAxix1 && yAxix2)
					await AddChartValuesToChart(item.TankId, item.TankName, item.ChartTypeName, item.Use2ndYaxis); // Both Y-axis in use
				else
					await AddChartValuesToChart(item.TankId, item.TankName, item.ChartTypeName, false); // Only one Y-axis
			}

			//            ChartingMain.ChartAreas[0].AxisX.ScaleView.Position = double.IsNaN(ChartingMain.ChartAreas[0].AxisX.ScaleView.ViewMinimum) ? 0:
			//                ChartingMain.ChartAreas[0].AxisX.ValueToPosition(ChartingMain.ChartAreas[0].AxisX.ScaleView.ViewMinimum);
			//ChartingMain.ChartAreas[0].AxisY.ScaleView.Position = double.IsNaN(ChartingMain.ChartAreas[0].AxisY.ScaleView.ViewMinimum) ? 0 :
			//                  ChartingMain.ChartAreas[0].AxisY.ValueToPosition(ChartingMain.ChartAreas[0].AxisY.ScaleView.ViewMinimum);
		}

		private async Task AddChartValuesToChart(int tankId, string tankName, string chartTypeName, bool use2ndYaxis)
		{
			// Init
			string chartSerie = tankName + " - " + chartTypeName;
			if (use2ndYaxis)
				chartSerie += " *";
			string chartOrder = "";
			// Get battle mode item, set param = "" if set as "ALL" Modes
			string chartMode = BattleChartHelper.Settings.BattleMode;
			// Check if already shown
			Series removeSerie = null;
			foreach (Series serie in ChartingMain.Series)
			{
				if (serie.Name == chartSerie)
					removeSerie = serie;
			}
			// Remove if shown, to add / refresh
			if (removeSerie != null)
				ChartingMain.Series.Remove(removeSerie);
			// Get Chart Value selected
			BattleChartHelper.ChartType chartType = chartTypeList.Find(c => c.name == chartTypeName);
			// Add series
			Series newSerie = new Series(chartSerie);
			// Check for 2nd Y-axis
			if (use2ndYaxis)
				newSerie.YAxisType = AxisType.Secondary;
			// Line  and marker type
			if (chartType != null)
			{
				newSerie.ChartType = chartType.seriesStyle;
				if (chartType.seriesStyle == SeriesChartType.Point) // Point = only dot shown
				{
					// Default marker type for point type
					newSerie.MarkerStyle = MarkerStyle.Circle;
				}
				else // Other chart type = line type
				{
					// Override to spline type if checked
					if (mSpline.Checked)
						newSerie.ChartType = SeriesChartType.Spline;
					// Set marker type
					if (mBullet.Checked)
						newSerie.MarkerStyle = MarkerStyle.Circle;
					else
						newSerie.MarkerStyle = MarkerStyle.None;
				}
				if (BattleChartHelper.Settings.Xaxis == "Date")
				{
					ChartingMain.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Auto;
					ChartingMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
					newSerie.XValueType = ChartValueType.DateTime;
					chartOrder = "DESC";
				}
				else if (BattleChartHelper.Settings.Xaxis == "Battle")
				{
					ChartingMain.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Number;
					ChartingMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
					newSerie.XValueType = ChartValueType.Int32;
				}

				// Add series to chart
				ChartingMain.Series.Add(newSerie);

				// Special actions / calculations for chart types
				switch (chartTypeName)
				{
					case "WN8":
						await DrawChartSeriesWN8(tankId, chartSerie, chartMode);
						return;
					case "WN9":
						if (tankId != 0)
							await DrawChartSeriesWN9PerTank(tankId, chartSerie, chartOrder, chartMode);
						else
							DrawChartSeriesWN9ForAccount(chartSerie, chartOrder, chartMode);
						return;
					default:
						if (chartTypeName.Length >= 4)
						{
							if (chartTypeName.Substring(0, 4) == "EMAi")
							{
								await DrawChartSeriesEMAi(tankId, chartSerie, chartOrder, chartType, chartMode, use2ndYaxis);
								return;
							}
						}
						break;
				}
				// Draw series in chart now
				await DrawChartSeries(tankId, chartSerie, chartOrder, chartType, chartMode, use2ndYaxis);
			}
		}

		private static double CalcChartSeriesPointValue(List<double> values, BattleChartHelper.CalculationType calcType, double defaultTier)
		{
			Code.Rating.WNHelper.RatingParameters rp = new Code.Rating.WNHelper.RatingParameters();
			double result = 0;
			switch (calcType)
			{
				case BattleChartHelper.CalculationType.standard:
					result = values[0];
					break;
				case BattleChartHelper.CalculationType.firstInPercentageOfNext:
					result = values[0] * 100 / values[1];
					break;
				case BattleChartHelper.CalculationType.firstDividedOnNext:
					result = values[0] / values[1];
					break;
				case BattleChartHelper.CalculationType.eff:
					rp.BATTLES = values[0];
					rp.DAMAGE = values[1];
					rp.SPOT = values[2];
					rp.FRAGS = values[3];
					rp.DEF = values[4];
					rp.CAP = values[5];
					rp.TIER = defaultTier; // values[6]; ???
					result = EFF.EffUseFormula(rp);
					break;
				case BattleChartHelper.CalculationType.wn7:
					rp.BATTLES = values[0];
					rp.DAMAGE = values[1];
					rp.SPOT = values[2];
					rp.FRAGS = values[3];
					rp.DEF = values[4];
					rp.CAP = values[5];
					rp.WINS = values[6];
					rp.TIER = defaultTier; // values[6]; ???
					result = WN7.WN7useFormula(rp);
					break;
				case BattleChartHelper.CalculationType.wn8:
					break;
				case BattleChartHelper.CalculationType.wn9:
					break;
				case BattleChartHelper.CalculationType.tierTotal:
					result = values[1] / values[0]; // sum tier / sum battles = total avg tier
					break;
				case BattleChartHelper.CalculationType.tierInterval:
					result = values[1] / values[0]; // sum tier / sum battles = total avg tier
					// Reset accumulated values to get interval on nexst
					values[1] = 0;
					values[0] = 0;
					break;
				case BattleChartHelper.CalculationType.EMAiN100:
					{
						// EMAi  = EMAi-1 + K*(value[i] –  EMAi-1)
						const double N = 100.0;
						const double k = 2.0 / (N + 1.0);
						result = values[0] + k * (values[1] - values[0]);
					}
					break;
				case BattleChartHelper.CalculationType.EMAiN10:
					{
						// EMAi  = EMAi-1 + K*(value[i] –  EMAi-1)
						// • k = 2 / (N +1)
						// • N = 10
						const double N = 10.0;
						const double k = 2.0 / (N + 1.0);
						result = values[0] + k * (values[1] - values[0]);
					}
					break;
				case BattleChartHelper.CalculationType.EMAiCombinedDmg:
					{
						// EMAi  = EMAi-1 + K*(value[i] –  EMAi-1)
						// • k = 2 / (N +1)
						// • N = 10
						const double N = 100.0;
						const double k = 2.0 / (N + 1.0);

						double combinedDamage = values[1] + Math.Max(values[2], values[3]);
						result = values[0] + k * (combinedDamage - values[0]);
					}
					break;
				default:
					break;
			}
			return result;
		}

		double SafeConvertToDouble(object value, double defaultValue = 0.0)
		{
			if (value != DBNull.Value)
				return Convert.ToDouble(value);
			else
				return defaultValue;
		}

		uint SafeConvertToUint(object value, uint defaultValue = 0)
		{
			if (value != DBNull.Value)
				return Convert.ToUInt32(value);
			else
				return defaultValue;
		}
		
		private async Task DrawChartSeries(int tankId, string chartSerie, string chartOrder, BattleChartHelper.ChartType chartType, string chartMode, bool use2ndYaxis)
		{
			// Create sql select fields and to store values
			string currentValCols = "";
			string firstValCols = "";
			string battleValCols = "";

			bool skipCommaCurrentValCols = true;
			bool skipCommaFirstValCols = true;
			bool skipCommaBattleValCols = true;

			List<double> values = new List<double>();
			foreach (BattleChartHelper.ChartTypeCols col in chartType.col)
			{
				if (col.battleValCol == "") col.battleValCol = col.playerTankValCol; // If no value added it is the same col on all sql's
				if (col.battleFirstValCol == "") col.battleFirstValCol = col.battleValCol; // If no value added use same as battle value col

				if (col.playerTankValCol != "")
				{
					if (skipCommaCurrentValCols)
					{
						skipCommaCurrentValCols = false;
					}
					else
					{
						currentValCols += ",";
					}

					currentValCols += "SUM(" + col.playerTankValCol + ")";
				}

				if (col.battleFirstValCol != "")
				{
					if (skipCommaFirstValCols)
					{
						skipCommaFirstValCols = false;
					}
					else
					{
						firstValCols += ",";
					}

					firstValCols += "SUM(" + col.battleFirstValCol + ")";
				}

				if (col.battleValCol != "")
				{
					if (skipCommaBattleValCols)
					{
						skipCommaBattleValCols = false;
					}
					else
					{
						battleValCols += ",";
					}

					battleValCols += col.battleValCol;
					values.Add(0);
				}
			}

			// Find playerTank current value or all tanks current value
			string ptWhere = "";
			string bWhere = "";
			if (tankId != 0)
			{
				// Find playertank and current value
				int playerTankId = await TankHelper.GetPlayerTankId(tankId);
				ptWhere = " where pt.id=@playerTankId ";
				bWhere = " where playerTankId=@playerTankId ";
				DB.AddWithValue(ref ptWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				DB.AddWithValue(ref bWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				if (chartMode != "ALL")
				{
					ptWhere += "and ptb.battleMode = '" + chartMode + "' ";
					bWhere += "and battleMode = '" + chartMode + "' ";
				}
			}
			else
			{
				if (chartMode != "ALL")
				{
					ptWhere += "where ptb.battleMode = '" + chartMode + "' ";
					bWhere += "where battleMode = '" + chartMode + "' ";
				}
			}

			// ---------------------------------------------------------
			// Get current values	
			// ---------------------------------------------------------
			string sql = "";
			double precomputedDefautTier = 0.0;
			if (currentValCols != "")
			{
				sql =
					"select " + currentValCols +
					" from playerTankBattle ptb" +
					" inner join playerTank pt on ptb.playerTankId=pt.id and pt.playerId=@playerId" +
					" inner join tank t on pt.tankId = t.id " +
					ptWhere;
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);

				DataTable dtCurrent = await DB.FetchData(sql);
				if (dtCurrent.Rows.Count > 0)
				{
					for (int i = 0; i < values.Count; i++)
					{
						values[i] = SafeConvertToDouble(dtCurrent.Rows[0][i]);
					}

					if ((chartType.calcType == BattleChartHelper.CalculationType.eff) && (tankId == 0))
					{
						// The total tier is added in column number 6, the total number of battles in col num 0
						precomputedDefautTier = Convert.ToDouble(dtCurrent.Rows[0][6]) / Convert.ToDouble(dtCurrent.Rows[0][0]);
					}
				}

				dtCurrent.Clear();

				// ---------------------------------------------------------
				// If chart series per battle, loop back to find first values
				// ---------------------------------------------------------
				if (firstValCols != "" && BattleChartHelper.Settings.Xaxis == "Battle")
				{
					// Find first value by sutracting sum of recorded values
					sql =
						"select " + firstValCols +
						" from battle b inner join " +
						"  playerTank pt on b.playerTankId=pt.id and pt.playerId=@playerId inner join " +
						"  tank t on pt.tankId = t.id " +
						GetBattleTimeFilter(bWhere);
					DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
					DataTable dtFirst = await DB.FetchData(sql);
					if (dtFirst.Rows.Count > 0)
					{
						for (int i = 0; i < values.Count; i++)
						{
							values[i] -= SafeConvertToDouble(dtFirst.Rows[0][i]);
						}
					}
					dtFirst.Clear();
				}
			}

			// ---------------------------------------------------------
			// Find battles
			// ---------------------------------------------------------
			sql =
				"select " + battleValCols + ", battleTime as battle_time, battlesCount as battles_Count " +
				"from battle b " +
				" inner join playerTank pt on b.playerTankId=pt.id and pt.playerId=@playerId " +
				" inner join tank t on pt.tankId = t.id " +
				GetBattleTimeFilter(bWhere) + " " +
				"order by battleTime " + chartOrder;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);

			DataTable dtChart = await DB.FetchData(sql);

			// Special actions / Calculations for special charts
			double defaultTier = 0;
			switch (chartType.calcType)
			{
				case BattleChartHelper.CalculationType.eff:
					if (tankId == 0)
						defaultTier = precomputedDefautTier;
					else
						defaultTier = await TankHelper.GetTankTier(tankId);
					break;
				case BattleChartHelper.CalculationType.wn7:
					if (tankId == 0)
						// The total tier is added in column number 6, the total number of battles in col num 0
						defaultTier = await WNHelper.GetAverageTier();
					else
						defaultTier = await TankHelper.GetTankTier(tankId);
					break;
			}
			// If show per date
			if (BattleChartHelper.Settings.Xaxis == "Date")
			{
				if (chartType.totals)
				{
					await DrawChartSeriesByDateUsingTotals(chartSerie, dtChart, values, chartType.calcType, defaultTier);
				}
				else
				{
					await DrawChartSeriesByDate(chartSerie, dtChart);
				}
			}

			// if show per battle (count)
			else if (BattleChartHelper.Settings.Xaxis == "Battle")
			{
				if (chartType.totals)
				{
					await DrawChartSeriesByBattleUsingTotals(chartSerie, dtChart, values, chartType.calcType, defaultTier);
				}
				else
				{
					await DrawChartSeriesByBattle(chartSerie, dtChart);
				}
			}

			dtChart.Clear();
		}
		private double firstValueFuncs(DataRow dr)
		{
			return SafeConvertToDouble(dr[0]);
		}

		private double totalsFuncDecrementValues(DataRow dr, List<double> computationValues, CalculationType calcType, double defaultTier)
		{
			double battleValue = Math.Round(CalcChartSeriesPointValue(computationValues, calcType, defaultTier), decimals);

			for (int i = 0; i < computationValues.Count; i++)
			{
				computationValues[i] -= SafeConvertToDouble(dr[i]);
			}

			return battleValue;
		}

		private double totalsFuncIncrementValues(DataRow dr, List<double> computationValues, CalculationType calcType, double defaultTier)
		{
			double battleValue = Math.Round(CalcChartSeriesPointValue(computationValues, calcType, defaultTier), decimals);

			for (int i = 0; i < computationValues.Count; i++)
			{
				computationValues[i] += SafeConvertToDouble(dr[i]);
			}

			return battleValue;
		}

		private async Task DrawChartSeriesByBattle(string chartSerie, DataTable dtChart)
		{
			await DrawChartSeriesByBattleGeneric(chartSerie, dtChart, dr => firstValueFuncs(dr));
		}

		private async Task DrawChartSeriesByBattleUsingTotals(string chartSerie, DataTable dtChart, List<double> computationValues, CalculationType calcType, double defaultTier)
		{
			await DrawChartSeriesByBattleGeneric(chartSerie, dtChart, dr => totalsFuncIncrementValues(dr, computationValues, calcType, defaultTier));
		}

		private async Task DrawChartSeriesByDate(string chartSerie, DataTable dtChart)
		{
			await DrawChartSeriesByDateGeneric(chartSerie, dtChart, dr => firstValueFuncs(dr));
		}

		private async Task DrawChartSeriesByDateUsingTotals(string chartSerie, DataTable dtChart, List<double> computationValues, CalculationType calcType, double defaultTier)
		{
			await DrawChartSeriesByDateGeneric(chartSerie, dtChart, dr => totalsFuncDecrementValues(dr, computationValues, calcType, defaultTier));
		}

		private async Task DrawChartSeriesByBattleGeneric(string chartSerie, DataTable dtChart, Func<DataRow, double> battleValueFunc)
		{
			int step = 0;
			int stepMod = dtChart.Rows.Count / numPoints;
			if (stepMod < 1) stepMod = 1;
			double battleNumber = 0;

			double value = 0.0;
			double count = 0.0;
			double chartVal = 0.0;

			List<double> values = new List<double>();
			foreach (DataRow dr in dtChart.Rows)
			{
				double rowBattleCount = Math.Max(1.0, SafeConvertToDouble(dr["battles_Count"]));
				battleNumber += rowBattleCount;

				double battleValue = battleValueFunc(dr);

				value += battleValue;
				count ++;

				if (step % stepMod == 0)
				{
					chartVal = value / count;

					ChartingMain.Series[chartSerie].Points.AddXY(battleNumber, chartVal);

					values.Add(chartVal);
					value = 0.0;
					count = 0.0;
				}

				step++;
			}
		}

		private async Task DrawChartSeriesByDateGeneric(string chartSerie, DataTable dtChart, Func<DataRow, double> battleValueFunc)
		{
			DateTime chartDate = DateTime.Now;
			double hourInterval = GetHourIntervalForChart(dtChart);

			double value = 0.0;
			double count = 0.0;
			double chartVal = 0.0;

			foreach (DataRow dr in dtChart.Rows)
			{
				DateTime thisDate = Convert.ToDateTime(dr["battle_time"]);
				double rowBattleCount = Math.Max(1.0, SafeConvertToDouble(dr["battles_Count"]));

				double battleValue = battleValueFunc(dr);

				value += battleValue;
				count ++;

				if (thisDate <= chartDate)
				{
					chartVal = value / count;

					ChartingMain.Series[chartSerie].Points.AddXY(thisDate, chartVal);

					chartDate = thisDate.AddHours(-hourInterval);
					count = 0.0f;
					value = 0.0f;
				}
			}
		}

		private async Task DrawChartSeriesWN8(int tankId, string chartSerie, string chartMode)
		{
			Cursor = Cursors.WaitCursor;
			// Find playerTank current value or all tanks current value
			string ptWhere = "";
			string bSumWhere = "";
			string bWhere = "";
			if (tankId != 0)
			{
				int playerTankId = await TankHelper.GetPlayerTankId(tankId);
				ptWhere = " and pt.id=@playerTankId ";
				bSumWhere = " and playerTankId=@playerTankId ";
				bWhere = " where playerTankId=@playerTankId ";
				DB.AddWithValue(ref ptWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				DB.AddWithValue(ref bSumWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				DB.AddWithValue(ref bWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			}
			string battleModeWhere = "";
			if (chartMode != "ALL")
				battleModeWhere = " and ptb.battleMode='" + chartMode + "' ";
			string sql =
				"select t.id as tankId, sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
				"  sum (ptb.def) as def, sum (ptb.cap) as cap, sum(wins) as wins " +
				"from playerTankBattle ptb left join " +
				"  playerTank pt on ptb.playerTankId=pt.id and pt.playerId=@playerId " + battleModeWhere + " left join " +
				"  tank t on pt.tankId = t.id " +
				"where t.expDmg is not null " + ptWhere + " " + battleModeWhere +
				"group by t.id ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable ptb = await DB.FetchData(sql); // ptb holds all parameters needed to calc WN8
			
			// Find battles
			if (chartMode != "ALL")
				battleModeWhere = " and battleMode='" + chartMode + "' ";
			sql =
				"select battle.*, playerTank.tankId as tankId, battleTime as battle_time, battlesCount as battles_Count " +
				"from battle inner join " +
				"  playerTank on battle.playerTankId = playerTank.id and playerTank.playerId=@playerId " +
				battleModeWhere +
				GetBattleTimeFilter(bWhere) + " " +
				"order by battleTime DESC "; 
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dtBattle = await DB.FetchData(sql);
			double chartVal = 0;

			// Draw chart per date
			if (BattleChartHelper.Settings.Xaxis == "Date")
			{
				DateTime chartDate = DateTime.Now;
				double hourInterval = GetHourIntervalForChart(dtBattle);
				
				foreach (DataRow drBattle in dtBattle.Rows)
				{
					DateTime thisDate = Convert.ToDateTime(drBattle["battle_time"]);
					if (thisDate <= chartDate)
					{
						chartVal = Math.Round(WN8.CalcPlayerTankBattle(ptb), decimals);
						ChartingMain.Series[chartSerie].Points.AddXY(thisDate, chartVal); // Use battle date
						chartDate = thisDate.AddHours(-hourInterval);
					}
					// Get prev val
					DataRow[] ptbRow = ptb.Select("tankId = " + drBattle["tankId"].ToString());
					if (ptbRow.Length > 0)
					{
						ptbRow[0]["battles"] = Convert.ToInt32(ptbRow[0]["battles"]) - Convert.ToInt32(drBattle["battlesCount"]);
						ptbRow[0]["dmg"] = Convert.ToInt32(ptbRow[0]["dmg"]) - Convert.ToInt32(drBattle["dmg"]);
						ptbRow[0]["spot"] = Convert.ToInt32(ptbRow[0]["spot"]) - Convert.ToInt32(drBattle["spotted"]);
						ptbRow[0]["frags"] = Convert.ToInt32(ptbRow[0]["frags"]) - Convert.ToInt32(drBattle["frags"]);
						ptbRow[0]["def"] = Convert.ToInt32(ptbRow[0]["def"]) - Convert.ToInt32(drBattle["def"]);
						ptbRow[0]["cap"] = Convert.ToInt32(ptbRow[0]["cap"]) - Convert.ToInt32(drBattle["cap"]);
						ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) - Convert.ToInt32(drBattle["victory"]);
					}
				}
			}
			
			// Draw chart per battle
			else if (BattleChartHelper.Settings.Xaxis == "Battle")
			{
				// Get total number of battles
				int battleTotalCount = 0;
				if (dtBattle.Rows.Count > 0)
					battleTotalCount = Convert.ToInt32(dtBattle.Compute("SUM(battlesCount)", "")) + 1;
				int step = 0;
				int stepMod = dtBattle.Rows.Count / numPoints;
				if (stepMod < 1) stepMod = 1;
				double battleCount = 0;
				// First point = Current val
				chartVal = Math.Round(Code.Rating.WN8.CalcPlayerTankBattle(ptb), decimals);
				ChartingMain.Series[chartSerie].Points.AddXY(battleTotalCount - battleCount, chartVal); 
				// Loop through battles and subtract values
				foreach (DataRow drBattle in dtBattle.Rows)
				{
					battleCount += Convert.ToDouble(drBattle["battlesCount"]); // Use battle count
					DataRow[] ptbRow = ptb.Select("tankId = " + drBattle["tankId"].ToString());
					if (ptbRow.Length > 0)
					{
						ptbRow[0]["battles"] = Convert.ToInt32(ptbRow[0]["battles"]) - Convert.ToInt32(drBattle["battlesCount"]); ;
						ptbRow[0]["dmg"] = Convert.ToInt32(ptbRow[0]["dmg"]) - Convert.ToInt32(drBattle["dmg"]);
						ptbRow[0]["spot"] = Convert.ToInt32(ptbRow[0]["spot"]) - Convert.ToInt32(drBattle["spotted"]);
						ptbRow[0]["frags"] = Convert.ToInt32(ptbRow[0]["frags"]) - Convert.ToInt32(drBattle["frags"]);
						ptbRow[0]["def"] = Convert.ToInt32(ptbRow[0]["def"]) - Convert.ToInt32(drBattle["def"]);
						ptbRow[0]["cap"] = Convert.ToInt32(ptbRow[0]["cap"]) - Convert.ToInt32(drBattle["cap"]);
						ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) - Convert.ToInt32(drBattle["victory"]);
					}
					step++;
					if (step % stepMod == 0)
					{
						chartVal = Math.Round(Code.Rating.WN8.CalcPlayerTankBattle(ptb), decimals);
						ChartingMain.Series[chartSerie].Points.AddXY(battleTotalCount - battleCount, chartVal);
					}
				}
				// Last point = First battle
				chartVal = Math.Round(Code.Rating.WN8.CalcPlayerTankBattle(ptb), decimals);
				ChartingMain.Series[chartSerie].Points.AddXY(battleTotalCount - battleCount, chartVal);
				// Force chart x-axis to start on 0
				ChartingMain.ChartAreas[0].AxisX.Minimum = 0;
			}
			Cursor = Cursors.Default;
		}
		
		private async Task DrawChartSeriesWN9PerTank(int tankId, string chartSerie, string chartOrder, string chartMode)
		{
			Cursor = Cursors.WaitCursor;
			// Find playerTank current value or all tanks current value
			string ptWhere = "";
			string bSumWhere = "";
			string bWhere = "";
			// Filter on tank
			int playerTankId = await TankHelper.GetPlayerTankId(tankId);
			ptWhere = " and pt.id=@playerTankId ";
			bSumWhere = " and playerTankId=@playerTankId ";
			bWhere = " where playerTankId=@playerTankId ";
			DB.AddWithValue(ref ptWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			DB.AddWithValue(ref bSumWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			DB.AddWithValue(ref bWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			// Filter on battlemode
			string battleModeWhere = "";
			if (chartMode != "ALL")
				battleModeWhere = " and ptb.battleMode='" + chartMode + "' ";
			// Get data
			string sql =
				"select t.id as tankId, sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
				"  sum (ptb.def) as def, sum (ptb.cap) as cap, sum(wins) as wins " +
				"from playerTankBattle ptb left join " +
				"  playerTank pt on ptb.playerTankId=pt.id and pt.playerId=@playerId " + battleModeWhere + " left join " +
				"  tank t on pt.tankId = t.id " +
				"where t.wn9exp is not null " + ptWhere + " " + battleModeWhere +
				"group by t.id ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable ptb = await DB.FetchData(sql);

			// if per battle, start from left - find initial value
			if (BattleChartHelper.Settings.Xaxis == "Battle")
			{
				// Find first value by sutracting sum of recorded values
				if (chartMode != "ALL")
					battleModeWhere = " and b.battleMode='" + chartMode + "' ";
				bSumWhere = "where t.expDmg is not null" + battleModeWhere + bSumWhere;
				sql =
					"select t.id as tankId, sum(b.battlesCount) as battles, sum(b.dmg) as dmg, sum (b.spotted) as spot, sum (b.frags) as frags, " +
					"  sum (b.def) as def, sum (cap) as cap, sum(victory) as wins " +
					"from battle b left join " +
					"  playerTank pt on b.playerTankId=pt.id and pt.playerId=@playerId left join " +
					"  tank t on pt.tankId = t.id " +
					GetBattleTimeFilter(bSumWhere) + " " +
					"group by t.id ";
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				DataTable dtBattleSum = await DB.FetchData(sql);
				if (dtBattleSum.Rows.Count > 0)
				{
					foreach (DataRow ptbRow in ptb.Rows)
					{
						DataRow[] bRow = dtBattleSum.Select("tankId = " + tankId);
						if (bRow.Length > 0)
						{
							ptbRow["battles"] = Convert.ToInt32(ptbRow["battles"]) - Convert.ToInt32(bRow[0]["battles"]);
							ptbRow["dmg"] = Convert.ToInt32(ptbRow["dmg"]) - Convert.ToInt32(bRow[0]["dmg"]);
							ptbRow["spot"] = Convert.ToInt32(ptbRow["spot"]) - Convert.ToInt32(bRow[0]["spot"]);
							ptbRow["frags"] = Convert.ToInt32(ptbRow["frags"]) - Convert.ToInt32(bRow[0]["frags"]);
							ptbRow["def"] = Convert.ToInt32(ptbRow["def"]) - Convert.ToInt32(bRow[0]["def"]);
							ptbRow["cap"] = Convert.ToInt32(ptbRow["cap"]) - Convert.ToInt32(bRow[0]["cap"]);
							ptbRow["wins"] = Convert.ToInt32(ptbRow["wins"]) - Convert.ToInt32(bRow[0]["wins"]);
						}
					}
				}
			}
			
			// Find battles
			if (chartMode != "ALL")
				battleModeWhere = " and battleMode='" + chartMode + "' ";
			sql =
				"select battle.*, playerTank.tankId as tankId, battleTime as battle_time, battlesCount as battles_Count " +
				"from battle inner join " +
				"  playerTank on battle.playerTankId = playerTank.id and playerTank.playerId=@playerId " + battleModeWhere +
				GetBattleTimeFilter(bWhere) + " " +
				"order by battleTime " + chartOrder;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dtChart = await DB.FetchData(sql);
			double chartVal = 0;
			
			// If per date
			if (BattleChartHelper.Settings.Xaxis == "Date")
			{
				DateTime chartDate = DateTime.Now;
				double hourInterval = GetHourIntervalForChart(dtChart);
				
				foreach (DataRow dr in dtChart.Rows)
				{
					DateTime thisDate = Convert.ToDateTime(dr["battle_time"]);
					if (thisDate <= chartDate)
					{
						chartVal = Math.Round((await WN9.CalcPlayerTankBattle(ptb)).WN9, decimals);
						ChartingMain.Series[chartSerie].Points.AddXY(thisDate, chartVal); // Use battle date
						chartDate = thisDate.AddHours(-hourInterval);
					}
					// Get prev val
					DataRow[] ptbRow = ptb.Select("tankId = " + tankId);
					if (ptbRow.Length > 0)
					{
						ptbRow[0]["battles"] = Convert.ToInt32(ptbRow[0]["battles"]) - Convert.ToInt32(dr["battlesCount"]);
						ptbRow[0]["dmg"] = Convert.ToInt32(ptbRow[0]["dmg"]) - Convert.ToInt32(dr["dmg"]);
						ptbRow[0]["spot"] = Convert.ToInt32(ptbRow[0]["spot"]) - Convert.ToInt32(dr["spotted"]);
						ptbRow[0]["frags"] = Convert.ToInt32(ptbRow[0]["frags"]) - Convert.ToInt32(dr["frags"]);
						ptbRow[0]["def"] = Convert.ToInt32(ptbRow[0]["def"]) - Convert.ToInt32(dr["def"]);
						ptbRow[0]["cap"] = Convert.ToInt32(ptbRow[0]["cap"]) - Convert.ToInt32(dr["cap"]);
						ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) - Convert.ToInt32(dr["victory"]);
					}
				}
			}
			// If per battle
			else if (BattleChartHelper.Settings.Xaxis == "Battle")
			{
				int step = 0;
				int stepMod = dtChart.Rows.Count / numPoints;
				if (stepMod < 1) stepMod = 1;
				double battleCount = 0;
				foreach (DataRow bRow in dtChart.Rows)
				{
					DataRow[] ptbRow = ptb.Select("tankId = " + tankId);
					battleCount += Convert.ToDouble(bRow["battlesCount"]); // Use battle count
					if (ptbRow.Length > 0)
					{
						ptbRow[0]["battles"] = Convert.ToInt32(ptbRow[0]["battles"]) + Convert.ToInt32(bRow["battlesCount"]); ;
						ptbRow[0]["dmg"] = Convert.ToInt32(ptbRow[0]["dmg"]) + Convert.ToInt32(bRow["dmg"]);
						ptbRow[0]["spot"] = Convert.ToInt32(ptbRow[0]["spot"]) + Convert.ToInt32(bRow["spotted"]);
						ptbRow[0]["frags"] = Convert.ToInt32(ptbRow[0]["frags"]) + Convert.ToInt32(bRow["frags"]);
						ptbRow[0]["def"] = Convert.ToInt32(ptbRow[0]["def"]) + Convert.ToInt32(bRow["def"]);
						ptbRow[0]["cap"] = Convert.ToInt32(ptbRow[0]["cap"]) + Convert.ToInt32(bRow["cap"]);
						ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) + Convert.ToInt32(bRow["victory"]);
					}
					step++;
					if (step % stepMod == 0 || step == 0)
					{
						WN9.Wn9Result Wn9result = await WN9.CalcPlayerTankBattle(ptb);
						chartVal = Math.Round(Wn9result.WN9, decimals);
						ChartingMain.Series[chartSerie].Points.AddXY(battleCount, chartVal); // Use battle count
					}
				}
			}
			Cursor = Cursors.Default;
		}

		private async Task DrawChartSeriesEMAi(int tankId, string chartSerie, string chartOrder, BattleChartHelper.ChartType chartType, string chartMode, bool use2ndYaxis)
		{
			// Create sql select fields and to store values
			string battleValCols = "";
			List<double> values = new List<double>();
			values.Add(0); // EMAi stores previously computed value in values[0]

			// Find playerTank current value or all tanks current value
			string ptWhere = "";
			string bWhere = "";
			string sql;
			foreach (BattleChartHelper.ChartTypeCols col in chartType.col)
			{
				if (col.battleValCol == "") col.battleValCol = col.playerTankValCol; // If no value added it is the same col on all sql's
				battleValCols += col.battleValCol + ",";
				values.Add(0);
			}

			battleValCols = battleValCols.Substring(0, battleValCols.Length - 1) + " "; // Remove latest comma

			if (tankId != 0)
			{
				// Find playertank and current value
				int playerTankId = await TankHelper.GetPlayerTankId(tankId);
				ptWhere = " where pt.id=@playerTankId ";
				bWhere = " where playerTankId=@playerTankId ";
				DB.AddWithValue(ref ptWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				DB.AddWithValue(ref bWhere, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				if (chartMode != "ALL")
				{
					ptWhere += "and ptb.battleMode = '" + chartMode + "' ";
					bWhere += "and battleMode = '" + chartMode + "' ";
				}
			}
			else
			{
				if (chartMode != "ALL")
				{
					ptWhere += "where ptb.battleMode = '" + chartMode + "' ";
					bWhere += "where battleMode = '" + chartMode + "' ";
				}
			}
						
			// Find battles
			sql =
				"select " + battleValCols + ", battleTime as battle_time, battlesCount as battles_Count " +
				"from battle b inner join " +
				"  playerTank pt on b.playerTankId=pt.id and pt.playerId=@playerId inner join " +
				"  tank t on pt.tankId = t.id " +
				GetBattleTimeFilter(bWhere) + " " +
				"order by battleTime " + chartOrder;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dtChart = await DB.FetchData(sql);

			// If show per date
			if (BattleChartHelper.Settings.Xaxis == "Date")
			{
				DateTime chartDate = DateTime.Now;
				double hourInterval = GetHourIntervalForChart(dtChart);
				
				double chartVal = 0;
				foreach (DataRow dr in dtChart.Rows)
				{
					DateTime thisDate = Convert.ToDateTime(dr["battle_time"]);

					double previousChartVal = chartVal;

					values[0] = previousChartVal;                    
					for (int i=1; i < values.Count; i++)
					{
						values[i] = SafeConvertToDouble(dr[i-1], values[i]); ;
					}

					chartVal = Math.Round(CalcChartSeriesPointValue(values, chartType.calcType, 0.0), decimals);

					if (thisDate <= chartDate)
					{
						ChartingMain.Series[chartSerie].Points.AddXY(thisDate, chartVal); // Use battle date
						chartDate = thisDate.AddHours(-hourInterval);
					}
				}
			}
			// if show per battle (count)
			else if (BattleChartHelper.Settings.Xaxis == "Battle")
			{
				int step = 0;
				int stepMod = dtChart.Rows.Count / numPoints;
				if (stepMod < 1) stepMod = 1;
				double battleCount = 0;

				double rowVal = 0.0;
				double chartVal = 0;
				foreach (DataRow dr in dtChart.Rows)
				{
					double rowBattleCount = Math.Max(1.0, SafeConvertToDouble(dr["battles_Count"]));
					battleCount += rowBattleCount;
																			 // Get next value
					double previousChartVal = chartVal;

					// In case of null object, return previous rowVal, (remove if we want to return 0.0)
					rowVal = SafeConvertToDouble(dr[0], rowVal);

					values[0] = previousChartVal;
					for (int i = 1; i < values.Count; i++)
					{
						values[i] = SafeConvertToDouble(dr[i - 1], values[i]);
					}

					chartVal = Math.Round(CalcChartSeriesPointValue(values, chartType.calcType, 0.0), decimals);

					step++;

					if (step % stepMod == 0 || step == 0)
					{
						ChartingMain.Series[chartSerie].Points.AddXY(battleCount, chartVal);
					}
				}
			}
			
			dtChart.Clear();
		}
		
		
		
		private void DrawChartSeriesWN9ForAccount(string chartSerie, string chartOrder, string chartMode)
		{
			// Start
			Cursor = Cursors.WaitCursor;

			// Done
			Cursor = Cursors.Default;

			MsgBox.Show("Not yet implemented, try WN9 per tank", "Not implemented");
			return;
			
		}

		Point? prevPosition = null;
		ToolTip tooltip = new ToolTip();

		private async void ChartingMain_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				string XLabel = BattleChartHelper.Settings.Xaxis + ": ";
				var pos = e.Location;
				if (prevPosition.HasValue && pos == prevPosition.Value)
					return;
				tooltip.RemoveAll();
				prevPosition = pos;
				var results = ChartingMain.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);
				foreach (var result in results)
				{
					if (result.ChartElementType == ChartElementType.DataPoint)
					{
						var prop = result.Object as DataPoint;
						if (prop != null)
						{
							var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
							var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);
							var pointY2Pixel = result.ChartArea.AxisY2.ValueToPixelPosition(prop.YValues[0]);

							// check if the cursor is really close to the point (2 pixels around the point)
							if (Math.Abs(pos.X - pointXPixel) < 2 && 
								(Math.Abs(pos.Y - pointYPixel) < 2 || Math.Abs(pos.Y - pointY2Pixel) < 2))
							{
								string TankName = result.Series.Name;
								string YValue = prop.YValues[0].ToString();
								string XValue = prop.XValue.ToString();
								if (BattleChartHelper.Settings.Xaxis == "Date")
								{
									XValue = DateTime.FromOADate((double)prop.XValue).ToString("dd.MM.yyyy");
								}
								tooltip.Show(
									TankName + Environment.NewLine +
									XLabel + XValue + Environment.NewLine + 
									"Value: " + YValue, 
									this.ChartingMain, pos.X + 10, pos.Y);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				await Log.LogToFile(ex);
				//throw;
			}
		}

		#endregion

		#region Toolbar events

		// Select Spline
		private async void mSpline_Click(object sender, EventArgs e)
		{
			mSpline.Checked = !mSpline.Checked;
			BattleChartHelper.Settings.Spline = mSpline.Checked;
			// Update chart view
			await DrawCurrentChartView();
			if (mSpline.Checked)
				lblFooter.Text = "Selected Spline (curved line)";
			else
				lblFooter.Text = "Selected Line";
		}

		// Select Bullet
		private async void mBullet_Click(object sender, EventArgs e)
		{
			mBullet.Checked = !mBullet.Checked;
			BattleChartHelper.Settings.Bullet = mBullet.Checked;
			// Update chart view
			await DrawCurrentChartView();
			if (mBullet.Checked)
				lblFooter.Text = "Selected Bullets";
			else
				lblFooter.Text = "Removed Bullets";
		}

		// Selected X-Axis
		private async void mXaxis_Click(object sender, EventArgs e)
		{
			ToolStripButton button = (ToolStripButton)sender;
			if (!button.Checked)
			{
				mXaxisBattle.Checked = false;
				mXaxisDate.Checked = false;
				button.Checked = true;
				BattleChartHelper.Settings.Xaxis = button.Text;
				// Update chart view
				await DrawCurrentChartView();
				lblFooter.Text = "Selected X-Axis";
			}
		}

		private void SetBattleTimeFilterMenu()
		{
			ToolStripItemCollection battleModeMenuList = mBattleTimeFilter.DropDownItems;
			foreach (var item in battleModeMenuList)
			{
				if (item is ToolStripMenuItem)
				{
					ToolStripMenuItem menuItem = (ToolStripMenuItem)item;
					menuItem.Checked = (menuItem.Tag.ToString() == BattleChartHelper.Settings.BattleTime);
					if (menuItem.Checked)
						mBattleTimeFilter.Text = menuItem.Text;
				}
			}
		}
			   

		private void SetBattleModeMenu()
		{
			string btlMode = BattleChartHelper.Settings.BattleMode;
			if (btlMode == "ALL") btlMode = "";
			mBattleModes.Text = BattleMode.GetItemFromSqlName(btlMode).Name;
			ToolStripItemCollection battleModeMenuList = mBattleModes.DropDownItems;
			foreach (var item in battleModeMenuList)
			{
				if (item is ToolStripMenuItem)
				{
					ToolStripMenuItem menuItem = (ToolStripMenuItem)item;
					menuItem.Checked = (menuItem.Tag.ToString() == BattleChartHelper.Settings.BattleMode);
				}
			}
		}

		// Selected Battle Time Filter
		private async void mBattleTimeChanged_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menu = (ToolStripMenuItem)sender;
			BattleChartHelper.Settings.BattleTime = menu.Tag.ToString();
			SetBattleTimeFilterMenu();
			// Update chart view
			await DrawCurrentChartView();
			lblFooter.Text = "Selected Battle Time: " + menu.Text;
		}

		// Select battle mode
		private async void mBattleModesChanged_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menu = (ToolStripMenuItem)sender;
			BattleChartHelper.Settings.BattleMode = menu.Tag.ToString();
			SetBattleModeMenu();
			// Update chart view
			await DrawCurrentChartView();
			lblFooter.Text = "Selected Battle Mode: " + menu.Text;
		}

		private void ClearChartArea(bool useTwoYaxis)
		{
			ChartingMain.Series.Clear();
			ChartingMain.ResetAutoValues();
			if (useTwoYaxis)
			{
				ChartingMain.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
				ChartingMain.ChartAreas[0].InnerPlotPosition = new ElementPosition(4, 0, 92, 91); // not so wide to allow 2nd Yxis to right
			}
			else
			{
				ChartingMain.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
				ChartingMain.ChartAreas[0].InnerPlotPosition = new ElementPosition(4, 0, 96, 91); // wide
			}
		}

		// Close form
		private void BattleChart_FormClosed(object sender, FormClosedEventArgs e)
		{
			FormHelper.ClosedOne();
		}

		// Clear Chart
		private async void mChartClear_Click(object sender, EventArgs e)
		{
			BattleChartHelper.CurrentChartView = new List<BattleChartHelper.BattleChartItem>();
			ClearChartArea(false);
			selectedChartFavouriteId = -1;
			selectedChartFavouriteName = "";
			BattleChartTheme.Text = "Chart";
			mFavouriteSave.Image = imageListToolStrip.Images[0];
			await SetFavouritMenu();
			SetFavouritMenuSelected(selectedChartFavouriteId);
			lblFooter.Text = "Chart view cleared, prepared for creating new chart";
			Refresh();
		}

		// Refresh
		private async void mRefresh_Click(object sender, EventArgs e)
		{
			// Update chart view
			await DrawCurrentChartView();
			lblFooter.Text = "Refreshed chart";
		}

		// Seleced menu button for adding new chart line
		private void mChartAdd_Click(object sender, EventArgs e)
		{
			OpenFormSelectChartParameters();
		}

		// Open form for adding new chart line
		private async void OpenFormSelectChartParameters(int tankId = 0)
		{
			Form frm = new ChartLineAdd(tankId);
			frm.ShowDialog();
			if (BattleChartHelper.NewChartItem.Count > 0)
			{
				// Add new chart items to chart view
				BattleChartHelper.CurrentChartView.AddRange(BattleChartHelper.NewChartItem);
				// Add the new chart item to current chart
				await DrawCurrentChartView();
				// Chage to unsaved
				mFavouriteSave.Image = imageListToolStrip.Images[1];
				mFavouriteSave.ToolTipText = "Update or Save as new Favourite (chart values unsaved)";

			}
			lblFooter.Text = "Added chart values";
		}

		private async void mChartRemove_Click(object sender, EventArgs e)
		{
			if (BattleChartHelper.CurrentChartView.Count == 0)
			{
				MsgBox.Show("There are no chart values to remove");
				return;
			}
			Form frm = new Forms.ChartLineRemove();
			frm.ShowDialog();
			if (BattleChartHelper.RemovedChartValues > 0)
			{
				await DrawCurrentChartView();
				lblFooter.Text = "Removed " + BattleChartHelper.RemovedChartValues + " chart value";
				if (BattleChartHelper.RemovedChartValues > 1)
					lblFooter.Text += "s";
				// Chage to unsaved
				mFavouriteSave.Image = imageListToolStrip.Images[1];
				mFavouriteSave.ToolTipText = "Update or Save as new Favourite (chart values unsaved)";
			}
		}

		private void mCheckBox_Paint(object sender, PaintEventArgs e)
		{
			ToolStripMenuItem menu = (ToolStripMenuItem)sender;
			if (menu.Checked)
			{
				// Default checkbox
				e.Graphics.DrawImage(Properties.Resources.menucheck, 5, 3);
				e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, ColorTheme.ToolGrayCheckBorder)), 4, 2, 17, 17);
				e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, ColorTheme.ToolGrayCheckBorder)), 5, 3, 15, 15);
			}
		}

		#endregion

		#region Favourites
		private async Task SetFavouritMenu()
		{
			ResetFavouriteMenu();
			string sql = "SELECT id, favouriteName FROM chartFav ORDER BY favouriteName;";
			DataTable dt = await DB.FetchData(sql);
			int count = dt.Rows.Count;
			if (count > 15)
				count = 15;
			mFavourite.Visible = (count > 0);
			if (count == 0)
				mFavouriteSave.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
			else
				mFavouriteSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
			for (int i = 1; i <= count; i++)
			{
				ToolStripItem[] menu = mFavourite.DropDownItems.Find("mFavourite" + i.ToString("00"), false);
				menu[0].Tag = dt.Rows[i - 1][0].ToString();
				menu[0].Text = dt.Rows[i - 1][1].ToString();
				menu[0].Visible = true;
			}
		}

		private void SetFavouritMenuSelected(int chartFavId)
		{
			for (int i = 1; i <= 15; i++)
			{
				ToolStripItem[] menu = mFavourite.DropDownItems.Find("mFavourite" + i.ToString("00"), false);
				ToolStripMenuItem menuItem = (ToolStripMenuItem)menu[0];
				menuItem.Checked = (Convert.ToInt32(menuItem.Tag) == chartFavId);
			}

		}

		private void ResetFavouriteMenu()
		{
			for (int i = 1; i <= 15; i++)
			{
				ToolStripItem[] menu = mFavourite.DropDownItems.Find("mFavourite" + i.ToString("00"), false);
				menu[0].Visible = false;
			}
			mFavouriteRemove.Enabled = (selectedChartFavouriteId != -1);
			mFavourite.Text = "";
		}

		private async void mFavouriteSelect_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menu = (ToolStripMenuItem)sender;
			await GetFavouriteChart(Convert.ToInt32(menu.Tag));
		}

		private async Task GetFavouriteChart(int chartFavId)
		{
			// Clear current charts
			BattleChartHelper.CurrentChartView = new List<BattleChartHelper.BattleChartItem>();
			// Get favourite
			string sql =
				"SELECT chartFav.*, chartFavLine.Id as chartFavLineId, chartFavLine.tankId, chartFavLine.chartTypeName, chartFavLine.use2ndYaxis " +
				"FROM chartFav LEFT JOIN chartFavLine ON chartFavLine.chartFavId = chartFav.Id " + 
				"WHERE chartFav.id = @id ORDER BY tankId, chartTypeName;";
			DB.AddWithValue(ref sql, "@id", chartFavId, DB.SqlDataType.Int);
			DataTable dt = await DB.FetchData(sql);
			// Read into chart if exists
			bool readChartFav = true;
			if (dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					// read chart fav settings only once
					if (readChartFav)
					{
						selectedChartFavouriteName = dr["favouriteName"].ToString();
						BattleChartHelper.Settings.BattleMode = dr["battleMode"].ToString();
						BattleChartHelper.Settings.BattleTime = dr["battleTime"].ToString();
						BattleChartHelper.Settings.Xaxis = dr["xaxis"].ToString();
						BattleChartHelper.Settings.Bullet = Convert.ToBoolean(dr["bullet"]);
						BattleChartHelper.Settings.Spline = Convert.ToBoolean(dr["spline"]);
						SetMenuItems();
						readChartFav = false;
					}
					if (dr["chartFavLineId"] != DBNull.Value)
					{
						BattleChartHelper.BattleChartItem item = new BattleChartHelper.BattleChartItem();
						item.ChartTypeName = dr["chartTypeName"].ToString();
						item.TankId = Convert.ToInt32(dr["tankId"]);
						item.TankName = TankHelper.GetTankName(item.TankId, true);
						item.Use2ndYaxis = Convert.ToBoolean(dr["use2ndYaxis"]);
						BattleChartHelper.CurrentChartView.Add(item);
					}
				}
				await DrawCurrentChartView();
				BattleChartTheme.Text = "Chart - " + selectedChartFavouriteName;
				selectedChartFavouriteId = chartFavId;
				lblFooter.Text = "Showing Favourite Chart: " + selectedChartFavouriteName;
			}
			else
			{
				// Chart not found, not existing
				selectedChartFavouriteId = -1;
				selectedChartFavouriteName = "";
				BattleChartTheme.Text = "Chart";
				ClearChartArea(false);
			}
			Refresh();
			// Set menu checked and show delete
			SetFavouritMenuSelected(selectedChartFavouriteId);
			mFavouriteRemove.Enabled = (selectedChartFavouriteId != -1);
			// Save status 
			mFavouriteSave.Image = imageListToolStrip.Images[0];
			mFavouriteSave.ToolTipText = "Update or Save as new Favourite";
		}

		private async void mFavouriteSave_Click(object sender, EventArgs e)
		{
			await SaveFavourite();
		}

		private async Task SaveFavourite()
		{
			if (BattleChartHelper.CurrentChartView.Count == 0)
			{
				MsgBox.Show("You have to add least one chart value before it is possible to save it as a favourite");
				return;
			}
			Form frm = new Chart.FavouriteSave(selectedChartFavouriteId, selectedChartFavouriteName);
			frm.ShowDialog();
			if (BattleChartHelper.SaveFavouriteSaved)
			{
				await SetFavouritMenu();
				SetFavouritMenuSelected(BattleChartHelper.SaveFavouriteNewFavId);
				BattleChartTheme.Text = "Chart - " + BattleChartHelper.SaveFavouriteNewFavName;
				selectedChartFavouriteId = BattleChartHelper.SaveFavouriteNewFavId;
				lblFooter.Text = "Saved favourite: " + BattleChartHelper.SaveFavouriteNewFavName;
				// Saved
				mFavouriteSave.Image = imageListToolStrip.Images[0];
				mFavouriteSave.ToolTipText = "Update or Save as new Favourite";
				mFavouriteRemove.Enabled = true;
			}
			else if (BattleChartHelper.SaveFavouriteDeleted)
			{
				await SetFavouritMenu();
				selectedChartFavouriteId = -1;
				SetFavouritMenuSelected(-1);
				BattleChartTheme.Text = "Chart";
				lblFooter.Text = "Deleted favourite";
				mFavouriteSave.Image = imageListToolStrip.Images[0];
				mFavouriteRemove.Enabled = false;
				BattleChartTheme.Text = "Chart";
			}
			Refresh();
		}
		
		#endregion

		private async void BattleChart_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (mFavouriteSave.ToolTipText == "Update or Save as new Favourite (chart values unsaved)" && BattleChartHelper.CurrentChartView.Count > 0)
			{
				MsgBox.Button answer = MsgBox.Show("Chart is edited but not saved as favourite, save now?", "Save Favourite", MsgBox.Type.YesNo);
				if (answer == MsgBox.Button.Yes)
				{
					await SaveFavourite();
				}
			}

			if (selectedChartFavouriteId.ToString() != Config.Settings.currentChartFavourite && selectedChartFavouriteId.ToString() != "-1")
			{
				Config.Settings.currentChartFavourite = selectedChartFavouriteId.ToString();
				await Config.SaveConfig();
			}

		}

		private async void mFavouriteRemove_Click(object sender, EventArgs e)
		{
			if (selectedChartFavouriteId > -1)
			{
				MsgBox.Button answer = MsgBox.Show("Are you sure you want to delete this favourite?", "Remove favourite", MsgBox.Type.YesNo, this);
				if (answer == MsgBox.Button.Yes)
				{
					await DB.ExecuteNonQuery(
					$"DELETE FROM chartFavLine WHERE chartFavId = {selectedChartFavouriteId};" +
					$"DELETE FROM chartFav WHERE id = {selectedChartFavouriteId};", false, true);
					selectedChartFavouriteId = -1;
					await SetFavouritMenu();
					SetFavouritMenuSelected(selectedChartFavouriteId);
					lblFooter.Text = "Deleted favourite";
					BattleChartTheme.Text = "Chart";
					Refresh();
				}
			}

		}
		private double GetHourIntervalForChart(DataTable dtChart)
		{
			double hourInterval = 24;
			if (dtChart.Rows.Count > 0)
			{
				DateTime chartDate = Convert.ToDateTime(dtChart.Rows[0]["battle_time"]);
				if (dtChart.Rows.Count > 5000)
					hourInterval = 48;
				else if (dtChart.Rows.Count > 10000)
					hourInterval = 24 * 7;
			}

			return hourInterval;
		}
	}
}
