using System;
using System.Collections.Generic;
using System.Linq;

namespace Archangel.Tests.BowlingGame.Infrastructure.Models
{
    public class Frame
    {
        private const short MaxAmountOfBallsForNormalFrame = 2;
        protected const short MaxAmountOfPins = 10;

        protected short SumOfTwoRoundPins = 0;

        public Frame()
        {
            PinsStruck = new List<short?>(MaxAmountOfBallsForNormalFrame);
        }

        public IList<short?> PinsStruck { get; protected set; }

        public short Score
        {
            get
            {
                var previousScore = PreviousFrame != null ? PreviousFrame.Score : 0;
                previousScore += SumOfTwoRoundPins;

                if (NextFrame != null)
                {
                    switch (Type)
                    {
                        case TypeOfFrame.Strike:
                            previousScore += NextFrame.ReturnAmountOfPins(2);
                            break;
                        case TypeOfFrame.Spare:
                            previousScore += NextFrame.ReturnAmountOfPins(1);
                            break;
                    }
                }

                return (short)previousScore;
            }
        }

        public virtual TypeOfFrame Type
        {
            get
            {
                if (PinsStruck.Count == 1 && SumOfTwoRoundPins == MaxAmountOfPins)
                    return TypeOfFrame.Strike;
                if (PinsStruck.Count == 2 && SumOfTwoRoundPins == MaxAmountOfPins)
                    return TypeOfFrame.Spare;
                return TypeOfFrame.Normal;
            }
        }

        public Frame NextFrame { get; set; }
        public Frame PreviousFrame { get; set; }

        public virtual bool IsAvailableToAddMorePins
        {
            get
            {
                if (Type == TypeOfFrame.Strike || Type == TypeOfFrame.Spare)
                    return false;

                if (PinsStruck.Count == MaxAmountOfBallsForNormalFrame)
                    return false;

                return true;
            }
        }

        public virtual bool TryToAddPins(short pins)
        {
            if (!IsAvailableToAddMorePins)
                return false;

            if (pins > MaxAmountOfPins || pins < 0)
                return false;

            if (SumOfTwoRoundPins + pins > MaxAmountOfPins)
                return false;

            PinsStruck.Add(pins);
            SumOfTwoRoundPins += pins;

            return true;
        }

        protected short ReturnAmountOfPins(short amountOfBalls)
        {
            if (PinsStruck.Count <= amountOfBalls)
            {
                short sum = (short)PinsStruck.Sum(s => s);
                amountOfBalls -= (short)PinsStruck.Count;
                if (NextFrame != null)
                    sum += NextFrame.ReturnAmountOfPins(amountOfBalls);
                return sum;
            }
            else
            {
                short sum = 0;
                for (int i = 0; i < amountOfBalls; i++)
                {
                    sum += PinsStruck[i].Value;
                }
                return sum;
            }
        }
    }
}
