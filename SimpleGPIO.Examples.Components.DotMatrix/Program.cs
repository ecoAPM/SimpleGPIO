using System.Threading.Tasks;
using SimpleGPIO.Boards;
using SimpleGPIO.Power;

namespace SimpleGPIO.Examples.Components.DotMatrix
{
    public static class Program
    {
        public static async Task Main()
        {
            using var pi = new RaspberryPi();
            var matrix = new SimpleGPIO.Components.DotMatrix(
                new SimpleGPIO.Components.DotMatrix.PinSet
                {
                    //rows
                    Pin1 = pi.GPIO5,
                    Pin2 = pi.GPIO7,
                    Pin3 = pi.GPIO12,
                    Pin4 = pi.GPIO13,
                    Pin5 = pi.GPIO8,
                    Pin6 = pi.GPIO15,
                    Pin7 = pi.GPIO6,
                    Pin8 = pi.GPIO3,

                    //columns
                    Pin9 = pi.GPIO1,
                    Pin10 = pi.GPIO14,
                    Pin11 = pi.GPIO16,
                    Pin12 = pi.GPIO4,
                    Pin13 = pi.GPIO11,
                    Pin14 = pi.GPIO2,
                    Pin15 = pi.GPIO17,
                    Pin16 = pi.GPIO18
                }
            );
            
            matrix.SetAllRows(PowerValue.Off);
            matrix.SetAllColumns(PowerValue.On);

            for (var x = 0; x < 8; x++)
            {
                if(x > 0)
                    matrix.Row[x-1].TurnOff();
                
                matrix.Row[x].TurnOn();
                await Task.Delay(100);
            }

            matrix.SetAllColumns(PowerValue.Off);
            matrix.SetAllRows(PowerValue.On);

            for (var x = 0; x < 8; x++)
            {
                if(x > 0)
                    matrix.Column[x-1].TurnOff();
                
                matrix.Column[x].TurnOn();
                await Task.Delay(100);
            }
            
            for(var x = 0; x < 3; x++)
            {
                matrix.SetAll(PowerValue.Off);
                await Task.Delay(100);

                matrix.SetRows(PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On, PowerValue.On, PowerValue.Off, PowerValue.Off, PowerValue.Off);
                matrix.SetColumns(PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On, PowerValue.On, PowerValue.Off, PowerValue.Off, PowerValue.Off);
                await Task.Delay(100);
                
                matrix.SetRows(PowerValue.Off, PowerValue.Off, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.Off, PowerValue.Off);
                matrix.SetColumns(PowerValue.Off, PowerValue.Off, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.Off, PowerValue.Off);
                await Task.Delay(100);
                
                matrix.SetRows(PowerValue.Off, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.Off);
                matrix.SetColumns(PowerValue.Off, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.Off);
                await Task.Delay(100);
                
                matrix.SetRows(PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On);
                matrix.SetColumns(PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On, PowerValue.On);
                await Task.Delay(100);
            }
            
            await Task.Delay(100);
            matrix.SetAll(PowerValue.Off);
        }
    }
}