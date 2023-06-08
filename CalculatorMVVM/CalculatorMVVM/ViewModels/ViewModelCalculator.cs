using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CalculatorMVVM.ViewModels
{
    
    public class ViewModelCalculator: ViewModelBase
    {
        #region Propiedades
        int currentState = 1;
        string result;
        public string Result
        {
            get { return result; }
            set
            {
                if (result != value)
                {
                    result = value;
                    OnPropertyChanged();
                }
            }
        }

        double firstNumber;
        public double FirstNumber
        {
            get { return firstNumber; }
            set
            {
                if(firstNumber != value)
                {
                    firstNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        double secondNumber;
        public double SecondNumber
        {
            get { return secondNumber; }
            set
            {
                if(secondNumber != value)
                {
                    secondNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        string mathOperator;
        public string MathOperator
        {
            get { return mathOperator; }
            set
            {
                if(mathOperator != value)
                {
                    mathOperator = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion


        public ICommand OnSelectNumber { protected set; get; }
        public ICommand OnClear { protected set; get; }
        public ICommand OnSelectOperator { protected set; get; }
        public ICommand OnCalculate { protected set; get; }
        public ViewModelCalculator()
        {
            OnClear = new Command(() =>
            {
                firstNumber = 0;
                secondNumber = 0;
                currentState = 1;
                Result = "0";
            });

            OnSelectNumber = new Command<string>(
                execute: (string parameter) =>
                {
                    
                    string pressed = parameter;

                    if (Result == "0" || currentState < 0)
                    {
                        Result = "";
                        if (currentState < 0)
                            currentState *= -1;
                    }

                    Result += pressed;

                    double number;
                    if (double.TryParse(Result, out number))
                    {
                        Result = number.ToString("N0");
                        if (currentState == 1)
                        {
                            firstNumber = number;
                        }
                        else
                        {
                            secondNumber = number;
                        }
                    }

                });
            OnSelectOperator = new Command<String>(
                execute: (string parameterName) =>
                {
                    string pressed = parameterName;

                    currentState = -2;
                    mathOperator = pressed;
                });

            OnCalculate = new Command(() =>
            {
                if(currentState == 2)
                {
                    var resultado = ViewModelSimpleCalculator.Calculate(firstNumber, SecondNumber, MathOperator);
                    Result = resultado.ToString();
                    firstNumber = resultado;
                    currentState = -1;
                }
            });
        }
    }
}
