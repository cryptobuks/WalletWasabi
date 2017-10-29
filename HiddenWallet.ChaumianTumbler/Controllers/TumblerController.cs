﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HiddenWallet.SharedApi.Models;
using HiddenWallet.ChaumianTumbler.Models;
using HiddenWallet.ChaumianCoinJoin;
using Org.BouncyCastle.Utilities.Encoders;
using NBitcoin;

namespace HiddenWallet.ChaumianTumbler.Controllers
{
	[Route("api/v1/[controller]")]
	public class TumblerController : Controller
    {
		[HttpGet]
		public string Test()
		{
			return "test";
		}

		//	Used to simulate a client connecting. Returns ClientTest.html which contains JavaScript
		//	to connect to the SignalR Hub - ChaumianTumblerHub. In real use the clients will connect 
		//	directly to the hub via C# code in HiddenWallet.
		[Route("client-test")]
		[HttpGet]
		public IActionResult ClientTest()
		{
			return View();
		}

		[Route("client-test-broadcast")]
		[HttpGet]
		public void TestBroadcast()
		{
			//	Trigger a call to the hub to broadcast a new state to the clients.
			TumblerPhaseBroadcaster tumblerPhaseBroadcast = TumblerPhaseBroadcaster.Instance;

			PhaseChangeBroadcast broadcast = new PhaseChangeBroadcast { NewPhase = TumblerPhase.OutputRegistration.ToString(), Message = "Just a test" };
			tumblerPhaseBroadcast.Broadcast(broadcast); //If collection.Count > 3 a SignalR broadcast is made to clients that connected via client-test
		}


		[Route("status")]
		[HttpGet]
		public IActionResult Status()
		{
			try
			{
				return new JsonResult(new StatusResponse
				{
					Phase = Global.StateMachine.Phase.ToString(),
					Denomination = Global.StateMachine.Denomination.ToString(fplus: false, trimExcessZero: true),
					AnonymitySet = Global.StateMachine.AnonymitySet,
					TimeSpentInInputRegistrationInSeconds = (int)Global.StateMachine.TimeSpentInInputRegistration.TotalSeconds
				});
			}
			catch (Exception ex)
			{
				return new ObjectResult(new FailureResponse { Message = ex.Message, Details = ex.ToString() });
			}
		}

		[Route("inputs")]
		[HttpPost]
		public IActionResult Inputs(InputsRequest request)
		{
			try
			{
				if (request.BlindedOutput == null) return new BadRequestResult();
				if (request.ChangeOutput == null) return new BadRequestResult();
				if (request.Inputs == null || request.Inputs.Count() == 0) return new BadRequestResult();
				byte[] blindedOutut = HexHelpers.GetBytes(request.BlindedOutput);
				var changeOutput = new BitcoinWitPubKeyAddress(request.ChangeOutput, expectedNetwork: Global.Config.Network);
				var inputs = new HashSet<OutPoint>();
				foreach (var input in request.Inputs)
				{
					OutPoint op = new OutPoint();
					op.FromHex(input.Input);
					inputs.Add(op);
				}

				// Check format (parse everyting))
				// Check if inputs are native segwit
				// Check if proofs are valid
				// Check if inputs have enough coins
				// Check if inputs are unspent
				// Check if inputs are confirmed or part of previous CoinJoin

				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				return new ObjectResult(new FailureResponse { Message = ex.Message, Details = ex.ToString() });
			}
		}

		[Route("input-registration-status")]
		[HttpGet]
		public IActionResult InputRegistrationStatus()
		{
			try
			{
				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				return new ObjectResult(new FailureResponse { Message = ex.Message, Details = ex.ToString() });
			}
		}

		[Route("connection-confirmation")]
		[HttpPost]
		public IActionResult ConnectionConfirmation(ConnectionConfirmationRequest request)
		{
			try
			{
				if (request.UniqueId == null) return new BadRequestResult();

				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				return new ObjectResult(new FailureResponse { Message = ex.Message, Details = ex.ToString() });
			}
		}

		[Route("output")]
		[HttpPost]
		public IActionResult Output(OutputRequest request)
		{
			try
			{
				if (request.SignedOutput == null) return new BadRequestResult();

				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				return new ObjectResult(new FailureResponse { Message = ex.Message, Details = ex.ToString() });
			}
		}

		[Route("coinjoin")]
		[HttpGet]
		public IActionResult CoinJoin()
		{
			try
			{
				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				return new ObjectResult(new FailureResponse { Message = ex.Message, Details = ex.ToString() });
			}
		}

		[Route("signature")]
		[HttpPost]
		public IActionResult Signature(SignatureRequest request)
		{
			try
			{
				if (request.Signature == null) return new BadRequestResult();
				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				return new ObjectResult(new FailureResponse { Message = ex.Message, Details = ex.ToString() });
			}
		}
	}
}