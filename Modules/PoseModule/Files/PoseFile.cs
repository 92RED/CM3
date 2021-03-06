﻿// Concept Matrix 3.
// Licensed under the MIT license.

namespace ConceptMatrix.PoseModule
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using ConceptMatrix.Services;

	public class PoseFile : FileBase
	{
		public static readonly FileType FileType = new FileType(@"cm3p", "CM3 Pose File", typeof(PoseFile));

		// These names must match the names of the Offsets.Bones class!
		public Transform Root { get; set; }
		public Transform Abdomen { get; set; }
		public Transform Throw { get; set; }
		public Transform Waist { get; set; }
		public Transform SpineA { get; set; }
		public Transform LegLeft { get; set; }
		public Transform LegRight { get; set; }
		public Transform HolsterLeft { get; set; }
		public Transform HolsterRight { get; set; }
		public Transform SheatheLeft { get; set; }
		public Transform SheatheRight { get; set; }
		public Transform SpineB { get; set; }
		public Transform ClothBackALeft { get; set; }
		public Transform ClothBackARight { get; set; }
		public Transform ClothFrontALeft { get; set; }
		public Transform ClothFrontARight { get; set; }
		public Transform ClothSideALeft { get; set; }
		public Transform ClothSideARight { get; set; }
		public Transform KneeLeft { get; set; }
		public Transform KneeRight { get; set; }
		public Transform BreastLeft { get; set; }
		public Transform BreastRight { get; set; }
		public Transform SpineC { get; set; }
		public Transform ClothBackBLeft { get; set; }
		public Transform ClothBackBRight { get; set; }
		public Transform ClothFrontBLeft { get; set; }
		public Transform ClothFrontBRight { get; set; }
		public Transform ClothSideBLeft { get; set; }
		public Transform ClothSideBRight { get; set; }
		public Transform CalfLeft { get; set; }
		public Transform CalfRight { get; set; }
		public Transform ScabbardLeft { get; set; }
		public Transform ScabbardRight { get; set; }
		public Transform Neck { get; set; }
		public Transform ClavicleLeft { get; set; }
		public Transform ClavicleRight { get; set; }
		public Transform ClothBackCLeft { get; set; }
		public Transform ClothBackCRight { get; set; }
		public Transform ClothFrontCLeft { get; set; }
		public Transform ClothFrontCRight { get; set; }
		public Transform ClothSideCLeft { get; set; }
		public Transform ClothSideCRight { get; set; }
		public Transform PoleynLeft { get; set; }
		public Transform PoleynRight { get; set; }
		public Transform FootLeft { get; set; }
		public Transform FootRight { get; set; }
		public Transform Head { get; set; }
		public Transform ArmLeft { get; set; }
		public Transform ArmRight { get; set; }
		public Transform PauldronLeft { get; set; }
		public Transform PauldronRight { get; set; }
		public Transform Unknown00 { get; set; }
		public Transform ToesLeft { get; set; }
		public Transform ToesRight { get; set; }
		public Transform HairA { get; set; }
		public Transform HairFrontLeft { get; set; }
		public Transform HairFrontRight { get; set; }
		public Transform EarLeft { get; set; }
		public Transform EarRight { get; set; }
		public Transform ForearmLeft { get; set; }
		public Transform ForearmRight { get; set; }
		public Transform ShoulderLeft { get; set; }
		public Transform ShoulderRight { get; set; }
		public Transform HairB { get; set; }
		public Transform HandLeft { get; set; }
		public Transform HandRight { get; set; }
		public Transform ShieldLeft { get; set; }
		public Transform ShieldRight { get; set; }
		public Transform EarringALeft { get; set; }
		public Transform EarringARight { get; set; }
		public Transform ElbowLeft { get; set; }
		public Transform ElbowRight { get; set; }
		public Transform CouterLeft { get; set; }
		public Transform CouterRight { get; set; }
		public Transform WristLeft { get; set; }
		public Transform WristRight { get; set; }
		public Transform IndexALeft { get; set; }
		public Transform IndexARight { get; set; }
		public Transform PinkyALeft { get; set; }
		public Transform PinkyARight { get; set; }
		public Transform RingALeft { get; set; }
		public Transform RingARight { get; set; }
		public Transform MiddleALeft { get; set; }
		public Transform MiddleARight { get; set; }
		public Transform ThumbALeft { get; set; }
		public Transform ThumbARight { get; set; }
		public Transform WeaponLeft { get; set; }
		public Transform WeaponRight { get; set; }
		public Transform EarringBLeft { get; set; }
		public Transform EarringBRight { get; set; }
		public Transform IndexBLeft { get; set; }
		public Transform IndexBRight { get; set; }
		public Transform PinkyBLeft { get; set; }
		public Transform PinkyBRight { get; set; }
		public Transform RingBLeft { get; set; }
		public Transform RingBRight { get; set; }
		public Transform MiddleBLeft { get; set; }
		public Transform MiddleBRight { get; set; }
		public Transform ThumbBLeft { get; set; }
		public Transform ThumbBRight { get; set; }
		public Transform TailA { get; set; }
		public Transform TailB { get; set; }
		public Transform TailC { get; set; }
		public Transform TailD { get; set; }
		public Transform TailE { get; set; }
		public Transform RootHead { get; set; }
		public Transform Jaw { get; set; }
		public Transform EyelidLowerLeft { get; set; }
		public Transform EyelidLowerRight { get; set; }
		public Transform EyeLeft { get; set; }
		public Transform EyeRight { get; set; }
		public Transform Nose { get; set; }
		public Transform CheekLeft { get; set; }
		public Transform HrothWhiskersLeft { get; set; }
		public Transform CheekRight { get; set; }
		public Transform HrothWhiskersRight { get; set; }
		public Transform LipsLeft { get; set; }
		public Transform HrothEyebrowLeft { get; set; }
		public Transform LipsRight { get; set; }
		public Transform HrothEyebrowRight { get; set; }
		public Transform EyebrowLeft { get; set; }
		public Transform HrothBridge { get; set; }
		public Transform EyebrowRight { get; set; }
		public Transform HrothBrowLeft { get; set; }
		public Transform Bridge { get; set; }
		public Transform HrothBrowRight { get; set; }
		public Transform BrowLeft { get; set; }
		public Transform HrothJawUpper { get; set; }
		public Transform BrowRight { get; set; }
		public Transform HrothLipUpper { get; set; }
		public Transform LipUpperA { get; set; }
		public Transform HrothEyelidUpperLeft { get; set; }
		public Transform EyelidUpperLeft { get; set; }
		public Transform HrothEyelidUpperRight { get; set; }
		public Transform EyelidUpperRight { get; set; }
		public Transform HrothLipsLeft { get; set; }
		public Transform LipLowerA { get; set; }
		public Transform HrothLipsRight { get; set; }
		public Transform VieraEar01ALeft { get; set; }
		public Transform LipUpperB { get; set; }
		public Transform HrothLipUpperLeft { get; set; }
		public Transform VieraEar01ARight { get; set; }
		public Transform LipLowerB { get; set; }
		public Transform HrothLipUpperRight { get; set; }
		public Transform VieraEar02ALeft { get; set; }
		public Transform HrothLipLower { get; set; }
		public Transform VieraEar02ARight { get; set; }
		public Transform VieraEar03ALeft { get; set; }
		public Transform VieraEar03ARight { get; set; }
		public Transform VieraEar04ALeft { get; set; }
		public Transform VieraEar04ARight { get; set; }
		public Transform VieraLipLowerA { get; set; }
		public Transform VieraLipUpperB { get; set; }
		public Transform VieraEar01BLeft { get; set; }
		public Transform VieraEar01BRight { get; set; }
		public Transform VieraEar02BLeft { get; set; }
		public Transform VieraEar02BRight { get; set; }
		public Transform VieraEar03BLeft { get; set; }
		public Transform VieraEar03BRight { get; set; }
		public Transform VieraEar04BLeft { get; set; }
		public Transform VieraEar04BRight { get; set; }
		public Transform VieraLipLowerB { get; set; }
		public Transform ExRootHair { get; set; }
		public Transform ExHairA { get; set; }
		public Transform ExHairB { get; set; }
		public Transform ExHairC { get; set; }
		public Transform ExHairD { get; set; }
		public Transform ExHairE { get; set; }
		public Transform ExHairF { get; set; }
		public Transform ExHairG { get; set; }
		public Transform ExHairH { get; set; }
		public Transform ExHairI { get; set; }
		public Transform ExHairJ { get; set; }
		public Transform ExHairK { get; set; }
		public Transform ExHairL { get; set; }
		public Transform ExRootMet { get; set; }
		public Transform ExMetA { get; set; }
		public Transform ExMetB { get; set; }
		public Transform ExMetC { get; set; }
		public Transform ExMetD { get; set; }
		public Transform ExMetE { get; set; }
		public Transform ExMetF { get; set; }
		public Transform ExMetG { get; set; }
		public Transform ExMetH { get; set; }
		public Transform ExMetI { get; set; }
		public Transform ExMetJ { get; set; }
		public Transform ExMetK { get; set; }
		public Transform ExMetL { get; set; }
		public Transform ExMetM { get; set; }
		public Transform ExMetN { get; set; }
		public Transform ExMetO { get; set; }
		public Transform ExMetP { get; set; }
		public Transform ExMetQ { get; set; }
		public Transform ExMetR { get; set; }
		public Transform ExRootTop { get; set; }
		public Transform ExTopA { get; set; }
		public Transform ExTopB { get; set; }
		public Transform ExTopC { get; set; }
		public Transform ExTopD { get; set; }
		public Transform ExTopE { get; set; }
		public Transform ExTopF { get; set; }
		public Transform ExTopG { get; set; }
		public Transform ExTopH { get; set; }
		public Transform ExTopI { get; set; }

		public override FileType GetFileType()
		{
			return FileType;
		}

		public void Read(IEnumerable<Bone> bones)
		{
			Type type = typeof(PoseFile);

			foreach (Bone bone in bones)
			{
				PropertyInfo prop = type.GetProperty(bone.BoneName);

				if (prop == null)
					throw new Exception("No save for bone: " + bone.BoneName);

				prop.SetValue(this, bone.Transform);
			}
		}

		public void Write(IEnumerable<Bone> bones)
		{
			Type type = typeof(PoseFile);

			foreach (Bone bone in bones)
			{
				PropertyInfo prop = type.GetProperty(bone.BoneName);

				if (prop == null)
					throw new Exception("No save for bone: " + bone.BoneName);

				Transform trans = (Transform)prop.GetValue(this);
				bone.Transform = trans;
			}
		}
	}
}
