using System.Collections.Generic;

namespace Archangel.Tests.BowlingGame.Infrastructure.Models
{
    public class LastFrame : Frame
    {
        private const short MaxAmountOfBallsForLastFrame = 3;

        public LastFrame()
        {
            PinsStruck = new List<short?>(MaxAmountOfBallsForLastFrame);
        }

        public override TypeOfFrame Type
        {
            get
            {
                if (PinsStruck.Count >= 1 && PinsStruck[0] == MaxAmountOfPins)
                    return TypeOfFrame.Strike;
                if (PinsStruck.Count >= 2 && PinsStruck[0] + PinsStruck[1] == MaxAmountOfPins)
                    return TypeOfFrame.Spare;
                return TypeOfFrame.Normal;
            }
        }

        public override bool IsAvailableToAddMorePins
        {
            get
            {
                if ((Type == TypeOfFrame.Strike || Type == TypeOfFrame.Spare) &&
                    PinsStruck.Count < MaxAmountOfBallsForLastFrame)
                    return true;
                if (Type == TypeOfFrame.Normal && PinsStruck.Count <= 1)
                    return true;

                return false;
            }
        }

        public override bool TryToAddPins(short pins)
        {
            if (!IsAvailableToAddMorePins)
                return false;

            if (pins > MaxAmountOfPins || pins < 0)
                return false;


            if (PinsStruck.Count == 1 && Type != TypeOfFrame.Strike & SumOfTwoRoundPins + pins > MaxAmountOfPins)
            {
                return false;
            }

            PinsStruck.Add(pins);
            SumOfTwoRoundPins += pins;

            return true;
        }
    }
}