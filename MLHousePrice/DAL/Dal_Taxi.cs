using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML;
using MLHousePrice.BLL;
using System.Windows.Forms;
using MLHousePrice.Interfaces;
using Microsoft.ML.Trainers.LightGbm;
using Microsoft.ML.Trainers.FastTree;

namespace MLHousePrice.DAL
{
    public class Dal_Taxi : ITaxi
    {
        public void TaxiModel()
        {
            MLContext mlContext = new MLContext(seed: 0);
           
            TextLoader textLoader = mlContext.Data.CreateTextLoader<TaxiTrip>(separatorChar: ',', hasHeader: true);

            IDataView trainingData = textLoader.Load("C:\\Users\\PC\\Downloads\\ML_Taxi_Project\\taxi-data-set.txt");

            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "FareAmount")                   
                   .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "VendorIdEncoded", inputColumnName: "VendorId"))
                   .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "RateCodeEncoded", inputColumnName: "RateCode"))
                   .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "PaymentTypeEncoded", inputColumnName: "PaymentType"))                   
                   .Append(mlContext.Transforms.Concatenate("Features", "VendorIdEncoded", "RateCodeEncoded", "PassengerCount", "TripDistance", "PaymentTypeEncoded"))                
                   .Append(mlContext.Regression.Trainers.FastTree());     
            var model = pipeline.Fit(trainingData);


            TextLoader textLoader1 = mlContext.Data.CreateTextLoader<TaxiTrip>(separatorChar: ',', hasHeader: true);
            IDataView trainingData1 = textLoader1.Load("C:\\Users\\PC\\Downloads\\ML_Taxi_Project\\taxi-data-set-test.txt");

        

            var testSetTransform = model.Transform(trainingData1);

            var metrics = mlContext.Regression.Evaluate(testSetTransform, "Label", "Score");

            MessageBox.Show($"RS {metrics.RSquared:#.##}\nMAE {metrics.MeanAbsoluteError:#.##} \nMSE {metrics.MeanSquaredError:#.##}\nRMSE {metrics.RootMeanSquaredError:#.##}");
            
            //   MessageBox.Show($"{metrics.MeanAbsoluteError:#.##}");

        }
    }
}
