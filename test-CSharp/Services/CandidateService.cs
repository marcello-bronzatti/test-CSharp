﻿using test_CSharp.Interfaces;
using test_CSharp.Interfaces.Repositories;
using test_CSharp.Models;

namespace test_CSharp.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _repository;

        public CandidateService(ICandidateRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Candidate>> GetCandidatesAsync()
        {
            return await _repository.GetCandidatesAsync();
        }

        public async Task<Candidate> GetCandidateByIdAsync(int id)
        {
            return await _repository.GetCandidateByIdAsync(id);
        }

        public async Task AddCandidate(Candidate candidate)
        {
            //Validate
            await _repository.AddCandidate(candidate);
            await _repository.SaveChangesAsync();

        }

        public async Task RemoveCandidate(int id)
        {
            var candidate = await _repository.GetCandidateByIdAsync(id);
            if (candidate == null)
                throw new DirectoryNotFoundException("Candidate not found");

            await _repository.RemoveCandidate(candidate);
            await _repository.SaveChangesAsync();

        }

        public async Task<Candidate> UpdateCandidate(Candidate candidate)
        {
            var candidateToUpdate = await _repository.GetCandidateByIdAsync(candidate.IdCandidate);
            if (candidateToUpdate == null)
                throw new DirectoryNotFoundException("Candidate not found");

            candidateToUpdate.Name = candidate.Name;
            candidateToUpdate.Surname = candidate.Surname;
            candidateToUpdate.Email = candidate.Email;
            candidateToUpdate.ModifyDate = DateTime.Now;

            await _repository.UpdateCandidate(candidateToUpdate);
            await _repository.SaveChangesAsync();

            return candidateToUpdate;
        }
    }
}
