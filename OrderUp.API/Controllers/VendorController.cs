using Microsoft.AspNetCore.Mvc;
using OrderUp.Application.DTOs;
using OrderUp.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class VendorsController : ControllerBase
{
  private readonly IVendorService _vendorService;

  public VendorsController(IVendorService vendorService)
  {
    _vendorService = vendorService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
      => Ok(await _vendorService.GetAllAsync());

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(Guid id)
  {
    var vendor = await _vendorService.GetByIdAsync(id);
    return vendor == null ? NotFound() : Ok(vendor);
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateVendorDto dto)
  {
    var vendor = await _vendorService.CreateAsync(dto);
    return CreatedAtAction(nameof(GetById), new { id = vendor.Id }, vendor);
  }
}
